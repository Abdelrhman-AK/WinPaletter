Imports WinPaletter.XenonCore
Imports System.IO

Public Class CursorsStudio
    Private _Shown As Boolean = False
    Private _SelectedControl As CursorControl
    Private _CopiedControl As CursorControl
    Private AnimateList As New List(Of CursorControl)

    Sub LoadFromCP([CP] As CP)
        XenonToggle1.Checked = [CP].Cursor_Enabled

#Region "Arrow"
        Arrow.Prop_PrimaryColor1 = [CP].Cursor_Arrow_PrimaryColor1
        Arrow.Prop_PrimaryColor2 = [CP].Cursor_Arrow_PrimaryColor2
        Arrow.Prop_PrimaryColorGradient = [CP].Cursor_Arrow_PrimaryColorGradient
        Arrow.Prop_PrimaryColorGradientMode = [CP].Cursor_Arrow_PrimaryColorGradientMode
        Arrow.Prop_PrimaryNoise = [CP].Cursor_Arrow_PrimaryColorNoise
        Arrow.Prop_PrimaryNoiseOpacity = [CP].Cursor_Arrow_PrimaryColorNoiseOpacity
        Arrow.Prop_SecondaryColor1 = [CP].Cursor_Arrow_SecondaryColor1
        Arrow.Prop_SecondaryColor2 = [CP].Cursor_Arrow_SecondaryColor2
        Arrow.Prop_SecondaryColorGradient = [CP].Cursor_Arrow_SecondaryColorGradient
        Arrow.Prop_SecondaryColorGradientMode = [CP].Cursor_Arrow_SecondaryColorGradientMode
        Arrow.Prop_SecondaryNoise = [CP].Cursor_Arrow_SecondaryColorNoise
        Arrow.Prop_SecondaryNoiseOpacity = [CP].Cursor_Arrow_SecondaryColorNoiseOpacity
#End Region

#Region "Help"
        Help.Prop_PrimaryColor1 = [CP].Cursor_Help_PrimaryColor1
        Help.Prop_PrimaryColor2 = [CP].Cursor_Help_PrimaryColor2
        Help.Prop_PrimaryColorGradient = [CP].Cursor_Help_PrimaryColorGradient
        Help.Prop_PrimaryColorGradientMode = [CP].Cursor_Help_PrimaryColorGradientMode
        Help.Prop_PrimaryNoise = [CP].Cursor_Help_PrimaryColorNoise
        Help.Prop_PrimaryNoiseOpacity = [CP].Cursor_Help_PrimaryColorNoiseOpacity
        Help.Prop_SecondaryColor1 = [CP].Cursor_Help_SecondaryColor1
        Help.Prop_SecondaryColor2 = [CP].Cursor_Help_SecondaryColor2
        Help.Prop_SecondaryColorGradient = [CP].Cursor_Help_SecondaryColorGradient
        Help.Prop_SecondaryColorGradientMode = [CP].Cursor_Help_SecondaryColorGradientMode
        Help.Prop_SecondaryNoise = [CP].Cursor_Help_SecondaryColorNoise
        Help.Prop_SecondaryNoiseOpacity = [CP].Cursor_Help_SecondaryColorNoiseOpacity

#End Region

#Region "AppLoading"
        AppLoading.Prop_PrimaryColor1 = [CP].Cursor_AppLoading_PrimaryColor1
        AppLoading.Prop_PrimaryColor2 = [CP].Cursor_AppLoading_PrimaryColor2
        AppLoading.Prop_PrimaryColorGradient = [CP].Cursor_AppLoading_PrimaryColorGradient
        AppLoading.Prop_PrimaryColorGradientMode = [CP].Cursor_AppLoading_PrimaryColorGradientMode
        AppLoading.Prop_PrimaryNoise = [CP].Cursor_AppLoading_PrimaryColorNoise
        AppLoading.Prop_PrimaryNoiseOpacity = [CP].Cursor_AppLoading_PrimaryColorNoiseOpacity
        AppLoading.Prop_SecondaryColor1 = [CP].Cursor_AppLoading_SecondaryColor1
        AppLoading.Prop_SecondaryColor2 = [CP].Cursor_AppLoading_SecondaryColor2
        AppLoading.Prop_SecondaryColorGradient = [CP].Cursor_AppLoading_SecondaryColorGradient
        AppLoading.Prop_SecondaryColorGradientMode = [CP].Cursor_AppLoading_SecondaryColorGradientMode
        AppLoading.Prop_SecondaryNoise = [CP].Cursor_AppLoading_SecondaryColorNoise
        AppLoading.Prop_SecondaryNoiseOpacity = [CP].Cursor_AppLoading_SecondaryColorNoiseOpacity
        AppLoading.Prop_LoadingCircleBack1 = [CP].Cursor_AppLoading_LoadingCircleBack1
        AppLoading.Prop_LoadingCircleBack2 = [CP].Cursor_AppLoading_LoadingCircleBack2
        AppLoading.Prop_LoadingCircleBackGradient = [CP].Cursor_AppLoading_LoadingCircleBackGradient
        AppLoading.Prop_LoadingCircleBackGradientMode = [CP].Cursor_AppLoading_LoadingCircleBackGradientMode
        AppLoading.Prop_LoadingCircleBackNoise = [CP].Cursor_AppLoading_LoadingCircleBackNoise
        AppLoading.Prop_LoadingCircleBackNoiseOpacity = [CP].Cursor_AppLoading_LoadingCircleBackNoiseOpacity
        AppLoading.Prop_LoadingCircleHot1 = [CP].Cursor_AppLoading_LoadingCircleHot1
        AppLoading.Prop_LoadingCircleHot2 = [CP].Cursor_AppLoading_LoadingCircleHot2
        AppLoading.Prop_LoadingCircleHotGradient = [CP].Cursor_AppLoading_LoadingCircleHotGradient
        AppLoading.Prop_LoadingCircleHotGradientMode = [CP].Cursor_AppLoading_LoadingCircleHotGradientMode
        AppLoading.Prop_LoadingCircleHotNoise = [CP].Cursor_AppLoading_LoadingCircleHotNoise
        AppLoading.Prop_LoadingCircleHotNoiseOpacity = [CP].Cursor_AppLoading_LoadingCircleHotNoiseOpacity

#End Region

#Region "Busy"
        Busy.Prop_LoadingCircleBack1 = [CP].Cursor_Busy_LoadingCircleBack1
        Busy.Prop_LoadingCircleBack2 = [CP].Cursor_Busy_LoadingCircleBack2
        Busy.Prop_LoadingCircleBackGradient = [CP].Cursor_Busy_LoadingCircleBackGradient
        Busy.Prop_LoadingCircleBackGradientMode = [CP].Cursor_Busy_LoadingCircleBackGradientMode
        Busy.Prop_LoadingCircleBackNoise = [CP].Cursor_Busy_LoadingCircleBackNoise
        Busy.Prop_LoadingCircleBackNoiseOpacity = [CP].Cursor_Busy_LoadingCircleBackNoiseOpacity
        Busy.Prop_LoadingCircleHot1 = [CP].Cursor_Busy_LoadingCircleHot1
        Busy.Prop_LoadingCircleHot2 = [CP].Cursor_Busy_LoadingCircleHot2
        Busy.Prop_LoadingCircleHotGradient = [CP].Cursor_Busy_LoadingCircleHotGradient
        Busy.Prop_LoadingCircleHotGradientMode = [CP].Cursor_Busy_LoadingCircleHotGradientMode
        Busy.Prop_LoadingCircleHotNoise = [CP].Cursor_Busy_LoadingCircleHotNoise
        Busy.Prop_LoadingCircleHotNoiseOpacity = [CP].Cursor_Busy_LoadingCircleHotNoiseOpacity

#End Region

#Region "Move"
        Move.Prop_PrimaryColor1 = [CP].Cursor_Move_PrimaryColor1
        Move.Prop_PrimaryColor2 = [CP].Cursor_Move_PrimaryColor2
        Move.Prop_PrimaryColorGradient = [CP].Cursor_Move_PrimaryColorGradient
        Move.Prop_PrimaryColorGradientMode = [CP].Cursor_Move_PrimaryColorGradientMode
        Move.Prop_PrimaryNoise = [CP].Cursor_Move_PrimaryColorNoise
        Move.Prop_PrimaryNoiseOpacity = [CP].Cursor_Move_PrimaryColorNoiseOpacity
        Move.Prop_SecondaryColor1 = [CP].Cursor_Move_SecondaryColor1
        Move.Prop_SecondaryColor2 = [CP].Cursor_Move_SecondaryColor2
        Move.Prop_SecondaryColorGradient = [CP].Cursor_Move_SecondaryColorGradient
        Move.Prop_SecondaryColorGradientMode = [CP].Cursor_Move_SecondaryColorGradientMode
        Move.Prop_SecondaryNoise = [CP].Cursor_Move_SecondaryColorNoise
        Move.Prop_SecondaryNoiseOpacity = [CP].Cursor_Move_SecondaryColorNoiseOpacity

#End Region

#Region "NS"
        NS.Prop_PrimaryColor1 = [CP].Cursor_NS_PrimaryColor1
        NS.Prop_PrimaryColor2 = [CP].Cursor_NS_PrimaryColor2
        NS.Prop_PrimaryColorGradient = [CP].Cursor_NS_PrimaryColorGradient
        NS.Prop_PrimaryColorGradientMode = [CP].Cursor_NS_PrimaryColorGradientMode
        NS.Prop_PrimaryNoise = [CP].Cursor_NS_PrimaryColorNoise
        NS.Prop_PrimaryNoiseOpacity = [CP].Cursor_NS_PrimaryColorNoiseOpacity
        NS.Prop_SecondaryColor1 = [CP].Cursor_NS_SecondaryColor1
        NS.Prop_SecondaryColor2 = [CP].Cursor_NS_SecondaryColor2
        NS.Prop_SecondaryColorGradient = [CP].Cursor_NS_SecondaryColorGradient
        NS.Prop_SecondaryColorGradientMode = [CP].Cursor_NS_SecondaryColorGradientMode
        NS.Prop_SecondaryNoise = [CP].Cursor_NS_SecondaryColorNoise
        NS.Prop_SecondaryNoiseOpacity = [CP].Cursor_NS_SecondaryColorNoiseOpacity
#End Region

#Region "EW"
        EW.Prop_PrimaryColor1 = [CP].Cursor_EW_PrimaryColor1
        EW.Prop_PrimaryColor2 = [CP].Cursor_EW_PrimaryColor2
        EW.Prop_PrimaryColorGradient = [CP].Cursor_EW_PrimaryColorGradient
        EW.Prop_PrimaryColorGradientMode = [CP].Cursor_EW_PrimaryColorGradientMode
        EW.Prop_PrimaryNoise = [CP].Cursor_EW_PrimaryColorNoise
        EW.Prop_PrimaryNoiseOpacity = [CP].Cursor_EW_PrimaryColorNoiseOpacity
        EW.Prop_SecondaryColor1 = [CP].Cursor_EW_SecondaryColor1
        EW.Prop_SecondaryColor2 = [CP].Cursor_EW_SecondaryColor2
        EW.Prop_SecondaryColorGradient = [CP].Cursor_EW_SecondaryColorGradient
        EW.Prop_SecondaryColorGradientMode = [CP].Cursor_EW_SecondaryColorGradientMode
        EW.Prop_SecondaryNoise = [CP].Cursor_EW_SecondaryColorNoise
        EW.Prop_SecondaryNoiseOpacity = [CP].Cursor_EW_SecondaryColorNoiseOpacity

#End Region

#Region "NESW"
        NESW.Prop_PrimaryColor1 = [CP].Cursor_NESW_PrimaryColor1
        NESW.Prop_PrimaryColor2 = [CP].Cursor_NESW_PrimaryColor2
        NESW.Prop_PrimaryColorGradient = [CP].Cursor_NESW_PrimaryColorGradient
        NESW.Prop_PrimaryColorGradientMode = [CP].Cursor_NESW_PrimaryColorGradientMode
        NESW.Prop_PrimaryNoise = [CP].Cursor_NESW_PrimaryColorNoise
        NESW.Prop_PrimaryNoiseOpacity = [CP].Cursor_NESW_PrimaryColorNoiseOpacity
        NESW.Prop_SecondaryColor1 = [CP].Cursor_NESW_SecondaryColor1
        NESW.Prop_SecondaryColor2 = [CP].Cursor_NESW_SecondaryColor2
        NESW.Prop_SecondaryColorGradient = [CP].Cursor_NESW_SecondaryColorGradient
        NESW.Prop_SecondaryColorGradientMode = [CP].Cursor_NESW_SecondaryColorGradientMode
        NESW.Prop_SecondaryNoise = [CP].Cursor_NESW_SecondaryColorNoise
        NESW.Prop_SecondaryNoiseOpacity = [CP].Cursor_NESW_SecondaryColorNoiseOpacity
#End Region

#Region "NWSE"
        NWSE.Prop_PrimaryColor1 = [CP].Cursor_NWSE_PrimaryColor1
        NWSE.Prop_PrimaryColor2 = [CP].Cursor_NWSE_PrimaryColor2
        NWSE.Prop_PrimaryColorGradient = [CP].Cursor_NWSE_PrimaryColorGradient
        NWSE.Prop_PrimaryColorGradientMode = [CP].Cursor_NWSE_PrimaryColorGradientMode
        NWSE.Prop_PrimaryNoise = [CP].Cursor_NWSE_PrimaryColorNoise
        NWSE.Prop_PrimaryNoiseOpacity = [CP].Cursor_NWSE_PrimaryColorNoiseOpacity
        NWSE.Prop_SecondaryColor1 = [CP].Cursor_NWSE_SecondaryColor1
        NWSE.Prop_SecondaryColor2 = [CP].Cursor_NWSE_SecondaryColor2
        NWSE.Prop_SecondaryColorGradient = [CP].Cursor_NWSE_SecondaryColorGradient
        NWSE.Prop_SecondaryColorGradientMode = [CP].Cursor_NWSE_SecondaryColorGradientMode
        NWSE.Prop_SecondaryNoise = [CP].Cursor_NWSE_SecondaryColorNoise
        NWSE.Prop_SecondaryNoiseOpacity = [CP].Cursor_NWSE_SecondaryColorNoiseOpacity

#End Region

#Region "Up"
        Up.Prop_PrimaryColor1 = [CP].Cursor_Up_PrimaryColor1
        Up.Prop_PrimaryColor2 = [CP].Cursor_Up_PrimaryColor2
        Up.Prop_PrimaryColorGradient = [CP].Cursor_Up_PrimaryColorGradient
        Up.Prop_PrimaryColorGradientMode = [CP].Cursor_Up_PrimaryColorGradientMode
        Up.Prop_PrimaryNoise = [CP].Cursor_Up_PrimaryColorNoise
        Up.Prop_PrimaryNoiseOpacity = [CP].Cursor_Up_PrimaryColorNoiseOpacity
        Up.Prop_SecondaryColor1 = [CP].Cursor_Up_SecondaryColor1
        Up.Prop_SecondaryColor2 = [CP].Cursor_Up_SecondaryColor2
        Up.Prop_SecondaryColorGradient = [CP].Cursor_Up_SecondaryColorGradient
        Up.Prop_SecondaryColorGradientMode = [CP].Cursor_Up_SecondaryColorGradientMode
        Up.Prop_SecondaryNoise = [CP].Cursor_Up_SecondaryColorNoise
        Up.Prop_SecondaryNoiseOpacity = [CP].Cursor_Up_SecondaryColorNoiseOpacity
#End Region

#Region "Pen"
        Pen.Prop_PrimaryColor1 = [CP].Cursor_Pen_PrimaryColor1
        Pen.Prop_PrimaryColor2 = [CP].Cursor_Pen_PrimaryColor2
        Pen.Prop_PrimaryColorGradient = [CP].Cursor_Pen_PrimaryColorGradient
        Pen.Prop_PrimaryColorGradientMode = [CP].Cursor_Pen_PrimaryColorGradientMode
        Pen.Prop_PrimaryNoise = [CP].Cursor_Pen_PrimaryColorNoise
        Pen.Prop_PrimaryNoiseOpacity = [CP].Cursor_Pen_PrimaryColorNoiseOpacity
        Pen.Prop_SecondaryColor1 = [CP].Cursor_Pen_SecondaryColor1
        Pen.Prop_SecondaryColor2 = [CP].Cursor_Pen_SecondaryColor2
        Pen.Prop_SecondaryColorGradient = [CP].Cursor_Pen_SecondaryColorGradient
        Pen.Prop_SecondaryColorGradientMode = [CP].Cursor_Pen_SecondaryColorGradientMode
        Pen.Prop_SecondaryNoise = [CP].Cursor_Pen_SecondaryColorNoise
        Pen.Prop_SecondaryNoiseOpacity = [CP].Cursor_Pen_SecondaryColorNoiseOpacity

#End Region

#Region "None"
        None.Prop_PrimaryColor1 = [CP].Cursor_None_PrimaryColor1
        None.Prop_PrimaryColor2 = [CP].Cursor_None_PrimaryColor2
        None.Prop_PrimaryColorGradient = [CP].Cursor_None_PrimaryColorGradient
        None.Prop_PrimaryColorGradientMode = [CP].Cursor_None_PrimaryColorGradientMode
        None.Prop_PrimaryNoise = [CP].Cursor_None_PrimaryColorNoise
        None.Prop_PrimaryNoiseOpacity = [CP].Cursor_None_PrimaryColorNoiseOpacity
        None.Prop_SecondaryColor1 = [CP].Cursor_None_SecondaryColor1
        None.Prop_SecondaryColor2 = [CP].Cursor_None_SecondaryColor2
        None.Prop_SecondaryColorGradient = [CP].Cursor_None_SecondaryColorGradient
        None.Prop_SecondaryColorGradientMode = [CP].Cursor_None_SecondaryColorGradientMode
        None.Prop_SecondaryNoise = [CP].Cursor_None_SecondaryColorNoise
        None.Prop_SecondaryNoiseOpacity = [CP].Cursor_None_SecondaryColorNoiseOpacity

#End Region

#Region "Link"
        Link.Prop_PrimaryColor1 = [CP].Cursor_Link_PrimaryColor1
        Link.Prop_PrimaryColor2 = [CP].Cursor_Link_PrimaryColor2
        Link.Prop_PrimaryColorGradient = [CP].Cursor_Link_PrimaryColorGradient
        Link.Prop_PrimaryColorGradientMode = [CP].Cursor_Link_PrimaryColorGradientMode
        Link.Prop_PrimaryNoise = [CP].Cursor_Link_PrimaryColorNoise
        Link.Prop_PrimaryNoiseOpacity = [CP].Cursor_Link_PrimaryColorNoiseOpacity
        Link.Prop_SecondaryColor1 = [CP].Cursor_Link_SecondaryColor1
        Link.Prop_SecondaryColor2 = [CP].Cursor_Link_SecondaryColor2
        Link.Prop_SecondaryColorGradient = [CP].Cursor_Link_SecondaryColorGradient
        Link.Prop_SecondaryColorGradientMode = [CP].Cursor_Link_SecondaryColorGradientMode
        Link.Prop_SecondaryNoise = [CP].Cursor_Link_SecondaryColorNoise
        Link.Prop_SecondaryNoiseOpacity = [CP].Cursor_Link_SecondaryColorNoiseOpacity
#End Region

#Region "Pin"
        Pin.Prop_PrimaryColor1 = [CP].Cursor_Pin_PrimaryColor1
        Pin.Prop_PrimaryColor2 = [CP].Cursor_Pin_PrimaryColor2
        Pin.Prop_PrimaryColorGradient = [CP].Cursor_Pin_PrimaryColorGradient
        Pin.Prop_PrimaryColorGradientMode = [CP].Cursor_Pin_PrimaryColorGradientMode
        Pin.Prop_PrimaryNoise = [CP].Cursor_Pin_PrimaryColorNoise
        Pin.Prop_PrimaryNoiseOpacity = [CP].Cursor_Pin_PrimaryColorNoiseOpacity
        Pin.Prop_SecondaryColor1 = [CP].Cursor_Pin_SecondaryColor1
        Pin.Prop_SecondaryColor2 = [CP].Cursor_Pin_SecondaryColor2
        Pin.Prop_SecondaryColorGradient = [CP].Cursor_Pin_SecondaryColorGradient
        Pin.Prop_SecondaryColorGradientMode = [CP].Cursor_Pin_SecondaryColorGradientMode
        Pin.Prop_SecondaryNoise = [CP].Cursor_Pin_SecondaryColorNoise
        Pin.Prop_SecondaryNoiseOpacity = [CP].Cursor_Pin_SecondaryColorNoiseOpacity

#End Region

#Region "Person"
        Person.Prop_PrimaryColor1 = [CP].Cursor_Person_PrimaryColor1
        Person.Prop_PrimaryColor2 = [CP].Cursor_Person_PrimaryColor2
        Person.Prop_PrimaryColorGradient = [CP].Cursor_Person_PrimaryColorGradient
        Person.Prop_PrimaryColorGradientMode = [CP].Cursor_Person_PrimaryColorGradientMode
        Person.Prop_PrimaryNoise = [CP].Cursor_Person_PrimaryColorNoise
        Person.Prop_PrimaryNoiseOpacity = [CP].Cursor_Person_PrimaryColorNoiseOpacity
        Person.Prop_SecondaryColor1 = [CP].Cursor_Person_SecondaryColor1
        Person.Prop_SecondaryColor2 = [CP].Cursor_Person_SecondaryColor2
        Person.Prop_SecondaryColorGradient = [CP].Cursor_Person_SecondaryColorGradient
        Person.Prop_SecondaryColorGradientMode = [CP].Cursor_Person_SecondaryColorGradientMode
        Person.Prop_SecondaryNoise = [CP].Cursor_Person_SecondaryColorNoise
        Person.Prop_SecondaryNoiseOpacity = [CP].Cursor_Person_SecondaryColorNoiseOpacity

#End Region

#Region "IBeam"
        IBeam.Prop_PrimaryColor1 = [CP].Cursor_IBeam_PrimaryColor1
        IBeam.Prop_PrimaryColor2 = [CP].Cursor_IBeam_PrimaryColor2
        IBeam.Prop_PrimaryColorGradient = [CP].Cursor_IBeam_PrimaryColorGradient
        IBeam.Prop_PrimaryColorGradientMode = [CP].Cursor_IBeam_PrimaryColorGradientMode
        IBeam.Prop_PrimaryNoise = [CP].Cursor_IBeam_PrimaryColorNoise
        IBeam.Prop_PrimaryNoiseOpacity = [CP].Cursor_IBeam_PrimaryColorNoiseOpacity
        IBeam.Prop_SecondaryColor1 = [CP].Cursor_IBeam_SecondaryColor1
        IBeam.Prop_SecondaryColor2 = [CP].Cursor_IBeam_SecondaryColor2
        IBeam.Prop_SecondaryColorGradient = [CP].Cursor_IBeam_SecondaryColorGradient
        IBeam.Prop_SecondaryColorGradientMode = [CP].Cursor_IBeam_SecondaryColorGradientMode
        IBeam.Prop_SecondaryNoise = [CP].Cursor_IBeam_SecondaryColorNoise
        IBeam.Prop_SecondaryNoiseOpacity = [CP].Cursor_IBeam_SecondaryColorNoiseOpacity
#End Region

#Region "Cross"
        Cross.Prop_PrimaryColor1 = [CP].Cursor_Cross_PrimaryColor1
        Cross.Prop_PrimaryColor2 = [CP].Cursor_Cross_PrimaryColor2
        Cross.Prop_PrimaryColorGradient = [CP].Cursor_Cross_PrimaryColorGradient
        Cross.Prop_PrimaryColorGradientMode = [CP].Cursor_Cross_PrimaryColorGradientMode
        Cross.Prop_PrimaryNoise = [CP].Cursor_Cross_PrimaryColorNoise
        Cross.Prop_PrimaryNoiseOpacity = [CP].Cursor_Cross_PrimaryColorNoiseOpacity
        Cross.Prop_SecondaryColor1 = [CP].Cursor_Cross_SecondaryColor1
        Cross.Prop_SecondaryColor2 = [CP].Cursor_Cross_SecondaryColor2
        Cross.Prop_SecondaryColorGradient = [CP].Cursor_Cross_SecondaryColorGradient
        Cross.Prop_SecondaryColorGradientMode = [CP].Cursor_Cross_SecondaryColorGradientMode
        Cross.Prop_SecondaryNoise = [CP].Cursor_Cross_SecondaryColorNoise
        Cross.Prop_SecondaryNoiseOpacity = [CP].Cursor_Cross_SecondaryColorNoiseOpacity
#End Region

        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then
                i.Invalidate()
            End If
        Next
    End Sub

    Sub SaveToCP([CP] As CP)
        [CP].Cursor_Enabled = XenonToggle1.Checked

#Region "Arrow"
        [CP].Cursor_Arrow_PrimaryColor1 = Arrow.Prop_PrimaryColor1
        [CP].Cursor_Arrow_PrimaryColor2 = Arrow.Prop_PrimaryColor2
        [CP].Cursor_Arrow_PrimaryColorGradient = Arrow.Prop_PrimaryColorGradient
        [CP].Cursor_Arrow_PrimaryColorGradientMode = Arrow.Prop_PrimaryColorGradientMode
        [CP].Cursor_Arrow_PrimaryColorNoise = Arrow.Prop_PrimaryNoise
        [CP].Cursor_Arrow_PrimaryColorNoiseOpacity = Arrow.Prop_PrimaryNoiseOpacity
        [CP].Cursor_Arrow_SecondaryColor1 = Arrow.Prop_SecondaryColor1
        [CP].Cursor_Arrow_SecondaryColor2 = Arrow.Prop_SecondaryColor2
        [CP].Cursor_Arrow_SecondaryColorGradient = Arrow.Prop_SecondaryColorGradient
        [CP].Cursor_Arrow_SecondaryColorGradientMode = Arrow.Prop_SecondaryColorGradientMode
        [CP].Cursor_Arrow_SecondaryColorNoise = Arrow.Prop_SecondaryNoise
        [CP].Cursor_Arrow_SecondaryColorNoiseOpacity = Arrow.Prop_SecondaryNoiseOpacity
#End Region

#Region "Help"
        [CP].Cursor_Help_PrimaryColor1 = Help.Prop_PrimaryColor1
        [CP].Cursor_Help_PrimaryColor2 = Help.Prop_PrimaryColor2
        [CP].Cursor_Help_PrimaryColorGradient = Help.Prop_PrimaryColorGradient
        [CP].Cursor_Help_PrimaryColorGradientMode = Help.Prop_PrimaryColorGradientMode
        [CP].Cursor_Help_PrimaryColorNoise = Help.Prop_PrimaryNoise
        [CP].Cursor_Help_PrimaryColorNoiseOpacity = Help.Prop_PrimaryNoiseOpacity
        [CP].Cursor_Help_SecondaryColor1 = Help.Prop_SecondaryColor1
        [CP].Cursor_Help_SecondaryColor2 = Help.Prop_SecondaryColor2
        [CP].Cursor_Help_SecondaryColorGradient = Help.Prop_SecondaryColorGradient
        [CP].Cursor_Help_SecondaryColorGradientMode = Help.Prop_SecondaryColorGradientMode
        [CP].Cursor_Help_SecondaryColorNoise = Help.Prop_SecondaryNoise
        [CP].Cursor_Help_SecondaryColorNoiseOpacity = Help.Prop_SecondaryNoiseOpacity

#End Region

#Region "AppLoading"
        [CP].Cursor_AppLoading_PrimaryColor1 = AppLoading.Prop_PrimaryColor1
        [CP].Cursor_AppLoading_PrimaryColor2 = AppLoading.Prop_PrimaryColor2
        [CP].Cursor_AppLoading_PrimaryColorGradient = AppLoading.Prop_PrimaryColorGradient
        [CP].Cursor_AppLoading_PrimaryColorGradientMode = AppLoading.Prop_PrimaryColorGradientMode
        [CP].Cursor_AppLoading_PrimaryColorNoise = AppLoading.Prop_PrimaryNoise
        [CP].Cursor_AppLoading_PrimaryColorNoiseOpacity = AppLoading.Prop_PrimaryNoiseOpacity
        [CP].Cursor_AppLoading_SecondaryColor1 = AppLoading.Prop_SecondaryColor1
        [CP].Cursor_AppLoading_SecondaryColor2 = AppLoading.Prop_SecondaryColor2
        [CP].Cursor_AppLoading_SecondaryColorGradient = AppLoading.Prop_SecondaryColorGradient
        [CP].Cursor_AppLoading_SecondaryColorGradientMode = AppLoading.Prop_SecondaryColorGradientMode
        [CP].Cursor_AppLoading_SecondaryColorNoise = AppLoading.Prop_SecondaryNoise
        [CP].Cursor_AppLoading_SecondaryColorNoiseOpacity = AppLoading.Prop_SecondaryNoiseOpacity
        [CP].Cursor_AppLoading_LoadingCircleBack1 = AppLoading.Prop_LoadingCircleBack1
        [CP].Cursor_AppLoading_LoadingCircleBack2 = AppLoading.Prop_LoadingCircleBack2
        [CP].Cursor_AppLoading_LoadingCircleBackGradient = AppLoading.Prop_LoadingCircleBackGradient
        [CP].Cursor_AppLoading_LoadingCircleBackGradientMode = AppLoading.Prop_LoadingCircleBackGradientMode
        [CP].Cursor_AppLoading_LoadingCircleBackNoise = AppLoading.Prop_LoadingCircleBackNoise
        [CP].Cursor_AppLoading_LoadingCircleBackNoiseOpacity = AppLoading.Prop_LoadingCircleBackNoiseOpacity
        [CP].Cursor_AppLoading_LoadingCircleHot1 = AppLoading.Prop_LoadingCircleHot1
        [CP].Cursor_AppLoading_LoadingCircleHot2 = AppLoading.Prop_LoadingCircleHot2
        [CP].Cursor_AppLoading_LoadingCircleHotGradient = AppLoading.Prop_LoadingCircleHotGradient
        [CP].Cursor_AppLoading_LoadingCircleHotGradientMode = AppLoading.Prop_LoadingCircleHotGradientMode
        [CP].Cursor_AppLoading_LoadingCircleHotNoise = AppLoading.Prop_LoadingCircleHotNoise
        [CP].Cursor_AppLoading_LoadingCircleHotNoiseOpacity = AppLoading.Prop_LoadingCircleHotNoiseOpacity

#End Region

#Region "Busy"
        [CP].Cursor_Busy_LoadingCircleBack1 = Busy.Prop_LoadingCircleBack1
        [CP].Cursor_Busy_LoadingCircleBack2 = Busy.Prop_LoadingCircleBack2
        [CP].Cursor_Busy_LoadingCircleBackGradient = Busy.Prop_LoadingCircleBackGradient
        [CP].Cursor_Busy_LoadingCircleBackGradientMode = Busy.Prop_LoadingCircleBackGradientMode
        [CP].Cursor_Busy_LoadingCircleBackNoise = Busy.Prop_LoadingCircleBackNoise
        [CP].Cursor_Busy_LoadingCircleBackNoiseOpacity = Busy.Prop_LoadingCircleBackNoiseOpacity
        [CP].Cursor_Busy_LoadingCircleHot1 = Busy.Prop_LoadingCircleHot1
        [CP].Cursor_Busy_LoadingCircleHot2 = Busy.Prop_LoadingCircleHot2
        [CP].Cursor_Busy_LoadingCircleHotGradient = Busy.Prop_LoadingCircleHotGradient
        [CP].Cursor_Busy_LoadingCircleHotGradientMode = Busy.Prop_LoadingCircleHotGradientMode
        [CP].Cursor_Busy_LoadingCircleHotNoise = Busy.Prop_LoadingCircleHotNoise
        [CP].Cursor_Busy_LoadingCircleHotNoiseOpacity = Busy.Prop_LoadingCircleHotNoiseOpacity

#End Region

#Region "Move"
        [CP].Cursor_Move_PrimaryColor1 = Move.Prop_PrimaryColor1
        [CP].Cursor_Move_PrimaryColor2 = Move.Prop_PrimaryColor2
        [CP].Cursor_Move_PrimaryColorGradient = Move.Prop_PrimaryColorGradient
        [CP].Cursor_Move_PrimaryColorGradientMode = Move.Prop_PrimaryColorGradientMode
        [CP].Cursor_Move_PrimaryColorNoise = Move.Prop_PrimaryNoise
        [CP].Cursor_Move_PrimaryColorNoiseOpacity = Move.Prop_PrimaryNoiseOpacity
        [CP].Cursor_Move_SecondaryColor1 = Move.Prop_SecondaryColor1
        [CP].Cursor_Move_SecondaryColor2 = Move.Prop_SecondaryColor2
        [CP].Cursor_Move_SecondaryColorGradient = Move.Prop_SecondaryColorGradient
        [CP].Cursor_Move_SecondaryColorGradientMode = Move.Prop_SecondaryColorGradientMode
        [CP].Cursor_Move_SecondaryColorNoise = Move.Prop_SecondaryNoise
        [CP].Cursor_Move_SecondaryColorNoiseOpacity = Move.Prop_SecondaryNoiseOpacity

#End Region

#Region "NS"
        [CP].Cursor_NS_PrimaryColor1 = NS.Prop_PrimaryColor1
        [CP].Cursor_NS_PrimaryColor2 = NS.Prop_PrimaryColor2
        [CP].Cursor_NS_PrimaryColorGradient = NS.Prop_PrimaryColorGradient
        [CP].Cursor_NS_PrimaryColorGradientMode = NS.Prop_PrimaryColorGradientMode
        [CP].Cursor_NS_PrimaryColorNoise = NS.Prop_PrimaryNoise
        [CP].Cursor_NS_PrimaryColorNoiseOpacity = NS.Prop_PrimaryNoiseOpacity
        [CP].Cursor_NS_SecondaryColor1 = NS.Prop_SecondaryColor1
        [CP].Cursor_NS_SecondaryColor2 = NS.Prop_SecondaryColor2
        [CP].Cursor_NS_SecondaryColorGradient = NS.Prop_SecondaryColorGradient
        [CP].Cursor_NS_SecondaryColorGradientMode = NS.Prop_SecondaryColorGradientMode
        [CP].Cursor_NS_SecondaryColorNoise = NS.Prop_SecondaryNoise
        [CP].Cursor_NS_SecondaryColorNoiseOpacity = NS.Prop_SecondaryNoiseOpacity
#End Region

#Region "EW"
        [CP].Cursor_EW_PrimaryColor1 = EW.Prop_PrimaryColor1
        [CP].Cursor_EW_PrimaryColor2 = EW.Prop_PrimaryColor2
        [CP].Cursor_EW_PrimaryColorGradient = EW.Prop_PrimaryColorGradient
        [CP].Cursor_EW_PrimaryColorGradientMode = EW.Prop_PrimaryColorGradientMode
        [CP].Cursor_EW_PrimaryColorNoise = EW.Prop_PrimaryNoise
        [CP].Cursor_EW_PrimaryColorNoiseOpacity = EW.Prop_PrimaryNoiseOpacity
        [CP].Cursor_EW_SecondaryColor1 = EW.Prop_SecondaryColor1
        [CP].Cursor_EW_SecondaryColor2 = EW.Prop_SecondaryColor2
        [CP].Cursor_EW_SecondaryColorGradient = EW.Prop_SecondaryColorGradient
        [CP].Cursor_EW_SecondaryColorGradientMode = EW.Prop_SecondaryColorGradientMode
        [CP].Cursor_EW_SecondaryColorNoise = EW.Prop_SecondaryNoise
        [CP].Cursor_EW_SecondaryColorNoiseOpacity = EW.Prop_SecondaryNoiseOpacity

#End Region

#Region "NESW"
        [CP].Cursor_NESW_PrimaryColor1 = NESW.Prop_PrimaryColor1
        [CP].Cursor_NESW_PrimaryColor2 = NESW.Prop_PrimaryColor2
        [CP].Cursor_NESW_PrimaryColorGradient = NESW.Prop_PrimaryColorGradient
        [CP].Cursor_NESW_PrimaryColorGradientMode = NESW.Prop_PrimaryColorGradientMode
        [CP].Cursor_NESW_PrimaryColorNoise = NESW.Prop_PrimaryNoise
        [CP].Cursor_NESW_PrimaryColorNoiseOpacity = NESW.Prop_PrimaryNoiseOpacity
        [CP].Cursor_NESW_SecondaryColor1 = NESW.Prop_SecondaryColor1
        [CP].Cursor_NESW_SecondaryColor2 = NESW.Prop_SecondaryColor2
        [CP].Cursor_NESW_SecondaryColorGradient = NESW.Prop_SecondaryColorGradient
        [CP].Cursor_NESW_SecondaryColorGradientMode = NESW.Prop_SecondaryColorGradientMode
        [CP].Cursor_NESW_SecondaryColorNoise = NESW.Prop_SecondaryNoise
        [CP].Cursor_NESW_SecondaryColorNoiseOpacity = NESW.Prop_SecondaryNoiseOpacity
#End Region

#Region "NWSE"
        [CP].Cursor_NWSE_PrimaryColor1 = NWSE.Prop_PrimaryColor1
        [CP].Cursor_NWSE_PrimaryColor2 = NWSE.Prop_PrimaryColor2
        [CP].Cursor_NWSE_PrimaryColorGradient = NWSE.Prop_PrimaryColorGradient
        [CP].Cursor_NWSE_PrimaryColorGradientMode = NWSE.Prop_PrimaryColorGradientMode
        [CP].Cursor_NWSE_PrimaryColorNoise = NWSE.Prop_PrimaryNoise
        [CP].Cursor_NWSE_PrimaryColorNoiseOpacity = NWSE.Prop_PrimaryNoiseOpacity
        [CP].Cursor_NWSE_SecondaryColor1 = NWSE.Prop_SecondaryColor1
        [CP].Cursor_NWSE_SecondaryColor2 = NWSE.Prop_SecondaryColor2
        [CP].Cursor_NWSE_SecondaryColorGradient = NWSE.Prop_SecondaryColorGradient
        [CP].Cursor_NWSE_SecondaryColorGradientMode = NWSE.Prop_SecondaryColorGradientMode
        [CP].Cursor_NWSE_SecondaryColorNoise = NWSE.Prop_SecondaryNoise
        [CP].Cursor_NWSE_SecondaryColorNoiseOpacity = NWSE.Prop_SecondaryNoiseOpacity

#End Region

#Region "Up"
        [CP].Cursor_Up_PrimaryColor1 = Up.Prop_PrimaryColor1
        [CP].Cursor_Up_PrimaryColor2 = Up.Prop_PrimaryColor2
        [CP].Cursor_Up_PrimaryColorGradient = Up.Prop_PrimaryColorGradient
        [CP].Cursor_Up_PrimaryColorGradientMode = Up.Prop_PrimaryColorGradientMode
        [CP].Cursor_Up_PrimaryColorNoise = Up.Prop_PrimaryNoise
        [CP].Cursor_Up_PrimaryColorNoiseOpacity = Up.Prop_PrimaryNoiseOpacity
        [CP].Cursor_Up_SecondaryColor1 = Up.Prop_SecondaryColor1
        [CP].Cursor_Up_SecondaryColor2 = Up.Prop_SecondaryColor2
        [CP].Cursor_Up_SecondaryColorGradient = Up.Prop_SecondaryColorGradient
        [CP].Cursor_Up_SecondaryColorGradientMode = Up.Prop_SecondaryColorGradientMode
        [CP].Cursor_Up_SecondaryColorNoise = Up.Prop_SecondaryNoise
        [CP].Cursor_Up_SecondaryColorNoiseOpacity = Up.Prop_SecondaryNoiseOpacity
#End Region

#Region "Pen"
        [CP].Cursor_Pen_PrimaryColor1 = Pen.Prop_PrimaryColor1
        [CP].Cursor_Pen_PrimaryColor2 = Pen.Prop_PrimaryColor2
        [CP].Cursor_Pen_PrimaryColorGradient = Pen.Prop_PrimaryColorGradient
        [CP].Cursor_Pen_PrimaryColorGradientMode = Pen.Prop_PrimaryColorGradientMode
        [CP].Cursor_Pen_PrimaryColorNoise = Pen.Prop_PrimaryNoise
        [CP].Cursor_Pen_PrimaryColorNoiseOpacity = Pen.Prop_PrimaryNoiseOpacity
        [CP].Cursor_Pen_SecondaryColor1 = Pen.Prop_SecondaryColor1
        [CP].Cursor_Pen_SecondaryColor2 = Pen.Prop_SecondaryColor2
        [CP].Cursor_Pen_SecondaryColorGradient = Pen.Prop_SecondaryColorGradient
        [CP].Cursor_Pen_SecondaryColorGradientMode = Pen.Prop_SecondaryColorGradientMode
        [CP].Cursor_Pen_SecondaryColorNoise = Pen.Prop_SecondaryNoise
        [CP].Cursor_Pen_SecondaryColorNoiseOpacity = Pen.Prop_SecondaryNoiseOpacity

#End Region

#Region "None"
        [CP].Cursor_None_PrimaryColor1 = None.Prop_PrimaryColor1
        [CP].Cursor_None_PrimaryColor2 = None.Prop_PrimaryColor2
        [CP].Cursor_None_PrimaryColorGradient = None.Prop_PrimaryColorGradient
        [CP].Cursor_None_PrimaryColorGradientMode = None.Prop_PrimaryColorGradientMode
        [CP].Cursor_None_PrimaryColorNoise = None.Prop_PrimaryNoise
        [CP].Cursor_None_PrimaryColorNoiseOpacity = None.Prop_PrimaryNoiseOpacity
        [CP].Cursor_None_SecondaryColor1 = None.Prop_SecondaryColor1
        [CP].Cursor_None_SecondaryColor2 = None.Prop_SecondaryColor2
        [CP].Cursor_None_SecondaryColorGradient = None.Prop_SecondaryColorGradient
        [CP].Cursor_None_SecondaryColorGradientMode = None.Prop_SecondaryColorGradientMode
        [CP].Cursor_None_SecondaryColorNoise = None.Prop_SecondaryNoise
        [CP].Cursor_None_SecondaryColorNoiseOpacity = None.Prop_SecondaryNoiseOpacity

#End Region

#Region "Link"
        [CP].Cursor_Link_PrimaryColor1 = Link.Prop_PrimaryColor1
        [CP].Cursor_Link_PrimaryColor2 = Link.Prop_PrimaryColor2
        [CP].Cursor_Link_PrimaryColorGradient = Link.Prop_PrimaryColorGradient
        [CP].Cursor_Link_PrimaryColorGradientMode = Link.Prop_PrimaryColorGradientMode
        [CP].Cursor_Link_PrimaryColorNoise = Link.Prop_PrimaryNoise
        [CP].Cursor_Link_PrimaryColorNoiseOpacity = Link.Prop_PrimaryNoiseOpacity
        [CP].Cursor_Link_SecondaryColor1 = Link.Prop_SecondaryColor1
        [CP].Cursor_Link_SecondaryColor2 = Link.Prop_SecondaryColor2
        [CP].Cursor_Link_SecondaryColorGradient = Link.Prop_SecondaryColorGradient
        [CP].Cursor_Link_SecondaryColorGradientMode = Link.Prop_SecondaryColorGradientMode
        [CP].Cursor_Link_SecondaryColorNoise = Link.Prop_SecondaryNoise
        [CP].Cursor_Link_SecondaryColorNoiseOpacity = Link.Prop_SecondaryNoiseOpacity
#End Region

#Region "Pin"
        [CP].Cursor_Pin_PrimaryColor1 = Pin.Prop_PrimaryColor1
        [CP].Cursor_Pin_PrimaryColor2 = Pin.Prop_PrimaryColor2
        [CP].Cursor_Pin_PrimaryColorGradient = Pin.Prop_PrimaryColorGradient
        [CP].Cursor_Pin_PrimaryColorGradientMode = Pin.Prop_PrimaryColorGradientMode
        [CP].Cursor_Pin_PrimaryColorNoise = Pin.Prop_PrimaryNoise
        [CP].Cursor_Pin_PrimaryColorNoiseOpacity = Pin.Prop_PrimaryNoiseOpacity
        [CP].Cursor_Pin_SecondaryColor1 = Pin.Prop_SecondaryColor1
        [CP].Cursor_Pin_SecondaryColor2 = Pin.Prop_SecondaryColor2
        [CP].Cursor_Pin_SecondaryColorGradient = Pin.Prop_SecondaryColorGradient
        [CP].Cursor_Pin_SecondaryColorGradientMode = Pin.Prop_SecondaryColorGradientMode
        [CP].Cursor_Pin_SecondaryColorNoise = Pin.Prop_SecondaryNoise
        [CP].Cursor_Pin_SecondaryColorNoiseOpacity = Pin.Prop_SecondaryNoiseOpacity

#End Region

#Region "Person"
        [CP].Cursor_Person_PrimaryColor1 = Person.Prop_PrimaryColor1
        [CP].Cursor_Person_PrimaryColor2 = Person.Prop_PrimaryColor2
        [CP].Cursor_Person_PrimaryColorGradient = Person.Prop_PrimaryColorGradient
        [CP].Cursor_Person_PrimaryColorGradientMode = Person.Prop_PrimaryColorGradientMode
        [CP].Cursor_Person_PrimaryColorNoise = Person.Prop_PrimaryNoise
        [CP].Cursor_Person_PrimaryColorNoiseOpacity = Person.Prop_PrimaryNoiseOpacity
        [CP].Cursor_Person_SecondaryColor1 = Person.Prop_SecondaryColor1
        [CP].Cursor_Person_SecondaryColor2 = Person.Prop_SecondaryColor2
        [CP].Cursor_Person_SecondaryColorGradient = Person.Prop_SecondaryColorGradient
        [CP].Cursor_Person_SecondaryColorGradientMode = Person.Prop_SecondaryColorGradientMode
        [CP].Cursor_Person_SecondaryColorNoise = Person.Prop_SecondaryNoise
        [CP].Cursor_Person_SecondaryColorNoiseOpacity = Person.Prop_SecondaryNoiseOpacity

#End Region

#Region "IBeam"
        [CP].Cursor_IBeam_PrimaryColor1 = IBeam.Prop_PrimaryColor1
        [CP].Cursor_IBeam_PrimaryColor2 = IBeam.Prop_PrimaryColor2
        [CP].Cursor_IBeam_PrimaryColorGradient = IBeam.Prop_PrimaryColorGradient
        [CP].Cursor_IBeam_PrimaryColorGradientMode = IBeam.Prop_PrimaryColorGradientMode
        [CP].Cursor_IBeam_PrimaryColorNoise = IBeam.Prop_PrimaryNoise
        [CP].Cursor_IBeam_PrimaryColorNoiseOpacity = IBeam.Prop_PrimaryNoiseOpacity
        [CP].Cursor_IBeam_SecondaryColor1 = IBeam.Prop_SecondaryColor1
        [CP].Cursor_IBeam_SecondaryColor2 = IBeam.Prop_SecondaryColor2
        [CP].Cursor_IBeam_SecondaryColorGradient = IBeam.Prop_SecondaryColorGradient
        [CP].Cursor_IBeam_SecondaryColorGradientMode = IBeam.Prop_SecondaryColorGradientMode
        [CP].Cursor_IBeam_SecondaryColorNoise = IBeam.Prop_SecondaryNoise
        [CP].Cursor_IBeam_SecondaryColorNoiseOpacity = IBeam.Prop_SecondaryNoiseOpacity
#End Region

#Region "Cross"
        [CP].Cursor_Cross_PrimaryColor1 = Cross.Prop_PrimaryColor1
        [CP].Cursor_Cross_PrimaryColor2 = Cross.Prop_PrimaryColor2
        [CP].Cursor_Cross_PrimaryColorGradient = Cross.Prop_PrimaryColorGradient
        [CP].Cursor_Cross_PrimaryColorGradientMode = Cross.Prop_PrimaryColorGradientMode
        [CP].Cursor_Cross_PrimaryColorNoise = Cross.Prop_PrimaryNoise
        [CP].Cursor_Cross_PrimaryColorNoiseOpacity = Cross.Prop_PrimaryNoiseOpacity
        [CP].Cursor_Cross_SecondaryColor1 = Cross.Prop_SecondaryColor1
        [CP].Cursor_Cross_SecondaryColor2 = Cross.Prop_SecondaryColor2
        [CP].Cursor_Cross_SecondaryColorGradient = Cross.Prop_SecondaryColorGradient
        [CP].Cursor_Cross_SecondaryColorGradientMode = Cross.Prop_SecondaryColorGradientMode
        [CP].Cursor_Cross_SecondaryColorNoise = Cross.Prop_SecondaryNoise
        [CP].Cursor_Cross_SecondaryColorNoiseOpacity = Cross.Prop_SecondaryNoiseOpacity
#End Region
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        MainFrm.Visible = False
        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)

        AnimateList.Clear()
        _CopiedControl = Nothing
        _Shown = False

        Angle = 180
        Cycles = 0
        Timer1.Enabled = False
        Timer1.Stop()

        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then
                AddHandler i.Click, AddressOf Clicked
                If i.Prop_Cursor = CursorType.AppLoading Or i.Prop_Cursor = CursorType.Busy Then AnimateList.Add(i)
            End If
        Next

        LoadFromCP(MainFrm.CP)
    End Sub

    Private Sub CursorsStudio_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub

    Private Sub CursorsStudio_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub Clicked(sender As Object, e As MouseEventArgs)
        _SelectedControl = DirectCast(sender, CursorControl)
        ApplyColorsFromCursor(_SelectedControl)

        XenonGroupBox2.Enabled = True
        XenonGroupBox11.Enabled = True
        XenonButton1.Enabled = True

        If _SelectedControl.Prop_Cursor = CursorType.AppLoading Or _SelectedControl.Prop_Cursor = CursorType.Busy Then

            If _SelectedControl.Prop_Cursor = CursorType.Busy Then
                XenonGroupBox2.Enabled = False
                XenonGroupBox11.Enabled = False
            End If

            XenonGroupBox6.Enabled = True
            XenonGroupBox12.Enabled = True
        Else
            XenonGroupBox6.Enabled = False
            XenonGroupBox12.Enabled = False
        End If

    End Sub

    Sub ApplyColorsFromCursor([CursorControl] As CursorControl)
        With [CursorControl]
            TaskbarFrontAndFoldersOnStart_picker.BackColor = .Prop_PrimaryColor1
            XenonGroupBox3.BackColor = .Prop_PrimaryColor2
            XenonCheckBox1.Checked = .Prop_PrimaryColorGradient
            XenonComboBox1.SelectedItem = ReturnStringFromGradientMode(.Prop_PrimaryColorGradientMode)
            XenonCheckBox5.Checked = .Prop_PrimaryNoise
            XenonNumericUpDown2.Text = .Prop_PrimaryNoiseOpacity * 100

            XenonGroupBox5.BackColor = .Prop_SecondaryColor1
            XenonGroupBox4.BackColor = .Prop_SecondaryColor2
            XenonCheckBox4.Checked = .Prop_SecondaryColorGradient
            XenonComboBox2.SelectedItem = ReturnStringFromGradientMode(.Prop_SecondaryColorGradientMode)
            XenonCheckBox3.Checked = .Prop_SecondaryNoise
            XenonNumericUpDown1.Text = .Prop_SecondaryNoiseOpacity * 100
            'XenonNumericUpDown3.Value = .Prop_LineThickness * 10

            XenonGroupBox10.BackColor = .Prop_LoadingCircleBack1
            XenonGroupBox9.BackColor = .Prop_LoadingCircleBack2
            XenonCheckBox8.Checked = .Prop_LoadingCircleBackGradient
            XenonComboBox4.SelectedItem = ReturnStringFromGradientMode(.Prop_LoadingCircleBackGradientMode)
            XenonCheckBox7.Checked = .Prop_LoadingCircleBackNoise
            XenonNumericUpDown6.Text = .Prop_LoadingCircleBackNoiseOpacity * 100

            XenonGroupBox8.BackColor = .Prop_LoadingCircleHot1
            XenonGroupBox7.BackColor = .Prop_LoadingCircleHot2
            XenonCheckBox2.Checked = .Prop_LoadingCircleHotGradient
            XenonComboBox3.SelectedItem = ReturnStringFromGradientMode(.Prop_LoadingCircleHotGradientMode)
            XenonCheckBox6.Checked = .Prop_LoadingCircleHotNoise
            XenonNumericUpDown4.Text = .Prop_LoadingCircleHotNoiseOpacity * 100
        End With

    End Sub

    Sub ApplyColorsToPreview([CursorControl] As CursorControl)
        With [CursorControl]
            .Prop_PrimaryColor1 = TaskbarFrontAndFoldersOnStart_picker.BackColor
            .Prop_PrimaryColor2 = XenonGroupBox3.BackColor
            .Prop_PrimaryColorGradient = XenonCheckBox1.Checked
            .Prop_PrimaryColorGradientMode = ReturnGradientModeFromString(XenonComboBox1.SelectedItem)
            .Prop_PrimaryNoise = XenonCheckBox5.Checked
            .Prop_PrimaryNoiseOpacity = Val(XenonNumericUpDown2.Text) / 100

            .Prop_SecondaryColor1 = XenonGroupBox5.BackColor
            .Prop_SecondaryColor2 = XenonGroupBox4.BackColor
            .Prop_SecondaryColorGradient = XenonCheckBox4.Checked
            .Prop_SecondaryColorGradientMode = ReturnGradientModeFromString(XenonComboBox2.SelectedItem)
            .Prop_SecondaryNoise = XenonCheckBox3.Checked
            .Prop_SecondaryNoiseOpacity = Val(XenonNumericUpDown1.Text) / 100
            '.Prop_LineThickness = XenonNumericUpDown3.Value / 10

            .Prop_LoadingCircleBack1 = XenonGroupBox10.BackColor
            .Prop_LoadingCircleBack2 = XenonGroupBox9.BackColor
            .Prop_LoadingCircleBackGradient = XenonCheckBox8.Checked
            .Prop_LoadingCircleBackGradientMode = ReturnGradientModeFromString(XenonComboBox4.SelectedItem)
            .Prop_LoadingCircleBackNoise = XenonCheckBox7.Checked
            .Prop_LoadingCircleBackNoiseOpacity = Val(XenonNumericUpDown6.Text) / 100

            .Prop_LoadingCircleHot1 = XenonGroupBox8.BackColor
            .Prop_LoadingCircleHot2 = XenonGroupBox7.BackColor
            .Prop_LoadingCircleHotGradient = XenonCheckBox2.Checked
            .Prop_LoadingCircleHotGradientMode = ReturnGradientModeFromString(XenonComboBox3.SelectedItem)
            .Prop_LoadingCircleHotNoise = XenonCheckBox6.Checked
            .Prop_LoadingCircleHotNoiseOpacity = Val(XenonNumericUpDown4.Text) / 100
        End With

    End Sub

    Private Sub TaskbarFrontAndFoldersOnStart_picker_Click(sender As Object, e As EventArgs) Handles TaskbarFrontAndFoldersOnStart_picker.Click

        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorBack1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_PrimaryColor1 = c
        _SelectedControl.Invalidate()

        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox3_Click(sender As Object, e As EventArgs) Handles XenonGroupBox3.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorBack2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_PrimaryColor2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox5_Click(sender As Object, e As EventArgs) Handles XenonGroupBox5.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorLine1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_SecondaryColor1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox4_Click(sender As Object, e As EventArgs) Handles XenonGroupBox4.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorLine2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_SecondaryColor2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox10_Click(sender As Object, e As EventArgs) Handles XenonGroupBox10.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorCircle1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleBack1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_PrimaryColorGradient = If(XenonCheckBox1.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox1.Invalidate()
    End Sub

    Private Sub XenonCheckBox4_CheckedChanged(sender As Object) Handles XenonCheckBox4.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryColorGradient = If(XenonCheckBox4.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox4.Invalidate()

    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_PrimaryColorGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox2.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryColorGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonCheckBox5_CheckedChanged(sender As Object) Handles XenonCheckBox5.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_PrimaryNoise = If(XenonCheckBox5.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox5.Invalidate()

    End Sub

    Private Sub XenonCheckBox3_CheckedChanged(sender As Object) Handles XenonCheckBox3.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryNoise = If(XenonCheckBox3.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox3.Invalidate()

    End Sub

    Private Sub XenonNumericUpDown3_Click(sender As Object, e As EventArgs)
        '_SelectedControl.Prop_LineThickness = sender.Value / 10
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        If Not _Shown Then Exit Sub

        For Each i As CursorControl In FlowLayoutPanel1.Controls
            i.Prop_Scale = sender.value / 100
            i.Width = 32 * i.Prop_Scale + 32
            i.Height = i.Width
            i.Refresh()
        Next

        Label5.Text = String.Format("Scaling ({0}x)", sender.value / 100)
    End Sub

    Dim Angle As Single = 180
    Dim Increment As Single = 5
    Dim Cycles As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not _Shown Then Exit Sub

        Try
            For Each i As CursorControl In AnimateList
                i.Angle = Angle
                i.Refresh()

                If Angle + Increment >= 360 Then Angle = 0
                Angle += Increment

                If Angle = 180 And Cycles >= 2 Then
                    i.Angle = 180
                    Timer1.Enabled = False
                    Timer1.Stop()
                ElseIf Angle = 180 Then
                    Cycles += 1
                End If

            Next
        Catch
        End Try
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Angle = 180
        Cycles = 0
        Timer1.Enabled = True
        Timer1.Start()
    End Sub

    Private Sub XenonGroupBox9_Click(sender As Object, e As EventArgs) Handles XenonGroupBox9.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorCircle2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleBack2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox8_Click(sender As Object, e As EventArgs) Handles XenonGroupBox8.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorCircleHot1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleHot1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox7_Click(sender As Object, e As EventArgs) Handles XenonGroupBox7.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorCircleHot2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleHot2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonCheckBox8_CheckedChanged(sender As Object) Handles XenonCheckBox8.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackGradient = If(XenonCheckBox8.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox8.Invalidate()
    End Sub

    Private Sub XenonCheckBox2_CheckedChanged(sender As Object) Handles XenonCheckBox2.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotGradient = If(XenonCheckBox2.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox2.Invalidate()
    End Sub

    Private Sub XenonCheckBox7_CheckedChanged(sender As Object) Handles XenonCheckBox7.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackNoise = If(XenonCheckBox7.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox7.Invalidate()

    End Sub

    Private Sub XenonCheckBox6_CheckedChanged(sender As Object) Handles XenonCheckBox6.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotNoise = If(XenonCheckBox6.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox6.Invalidate()

    End Sub

    Private Sub XenonComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox4.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox3.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        _CopiedControl = _SelectedControl
        XenonButton2.Enabled = True
        XenonButton6.Enabled = True

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        ApplyColorsFromCursor(_CopiedControl)
        ApplyColorsToPreview(_SelectedControl)
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then
                ApplyColorsFromCursor(_CopiedControl)
                ApplyColorsToPreview(i)
                i.Invalidate()
            End If
        Next
    End Sub

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonNumericUpDown2.TextChanged
        If Not _Shown Then Exit Sub

        Dim valX As Single = Val(sender.Text)
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_PrimaryNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonTextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles XenonNumericUpDown6.TextChanged
        If Not _Shown Then Exit Sub

        Dim valX As Single = Val(sender.Text)
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_LoadingCircleBackNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonTextBox1_TextChanged_2(sender As Object, e As EventArgs) Handles XenonNumericUpDown1.TextChanged
        If Not _Shown Then Exit Sub

        Dim valX As Single = Val(sender.Text)
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_SecondaryNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonTextBox1_TextChanged_3(sender As Object, e As EventArgs) Handles XenonNumericUpDown4.TextChanged
        If Not _Shown Then Exit Sub

        Dim valX As Single = Val(sender.Text)
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_LoadingCircleHotNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Me.Close()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        SaveToCP(MainFrm.CP)
        Me.Close()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            LoadFromCP(New CP(CP.Mode.File, OpenFileDialog1.FileName))
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        LoadFromCP(New CP(CP.Mode.Registry))
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        LoadFromCP(New CP(CP.Mode.Init))
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        MsgBox(My.Application.LanguageHelper.ScalingTip, MsgBoxStyle.Information + My.Application.MsgboxRt)
    End Sub
End Class