
Imports System.Reflection
Imports WinPaletter.XenonCore

Public Class Localizer

#Region "General"
    Public Property A1 As String = "Apply"
    Public Property A2 As String = "OK"
    Public Property A3 As String = "Start"
    Public Property A4 As String = "Refresh"
    Public Property A5 As String = "Save"
    Public Property A6 As String = "Cancel"
    Public Property A7 As String = "Close"
    Public Property A8 As String = "Uninstall"
    Public Property A9 As String = "Settings"
    Public Property A10 As String = "Updates"
    Public Property A11 As String = "About"
    Public Property A12 As String = "Preview"
    Public Property A13 As String = "Current Mode"
    Public Property A14 As String = "Init"
    Public Property A15 As String = "Not Used"
    Public Property A16 As String = "Open Settings"
    Public Property A17 As String = "Microsoft Windows 11"
    Public Property A18 As String = "Microsoft Windows 10"
    Public Property A19 As String = "Load these info"
    Public Property A20 As String = "By"
    Public Property A21 As String = "All Rights Reserved"
    Public Property A22 As String = "Github"
    Public Property A23 As String = "Reddit"
    Public Property A24 As String = "Version"
    Public Property A25 As String = "Channel"
    Public Property A26 As String = "MB"
    Public Property A27 As String = "Stable"
    Public Property A28 As String = "Beta"
    Public Property A29 As String = "Recommended"
    Public Property A30 As String = "Not Recommended"
    Public Property A31 As String = "No Network is avaliable"
    Public Property A32 As String = "Check your connection and try again"
#End Region

#Region "Main Form"
    Public Property B1 As String = "Toggles"
    Public Property B2 As String = "Windows Mode"
    Public Property B3 As String = "App Mode"
    Public Property B4 As String = "Transparency (Mica/Acrylic)"
    Public Property B5 As String = "Accent Color on Start, Taskbar and Action Center"
    Public Property B6 As String = "Titlebars"
    Public Property B7 As String = "Active Titlebar"
    Public Property B8 As String = "Inactive Titlebar"
    Public Property B9 As String = "Accents"
    Public Property B10 As String = "See Also"
    Public Property B11 As String = "Win32UI Elements"
    Public Property B12 As String = "LogonUI Screen"
    Public Property B13 As String = "Create New Theme (Palette) File"
    Public Property B14 As String = "Open a Theme File"
    Public Property B15 As String = "Save Theme File"
    Public Property B16 As String = "Save Theme File as ..."
    Public Property B17 As String = "Edit Information of current Theme File"
    Public Property B18 As String = "Q\A"
    Public Property B19 As String = "App Preview"
    Public Property B20 As String = "Inactive app"
    Public Property B21 As String = "This is a setting icon"
    Public Property B22 As String = "Link Preview"
    Public Property B23 As String = "Theme (1.0)"
    Public Property B24 As String = "New Update Avaliable"
    Public Property B25 As String = "Open Updates form for actions."

    Public Property B26 As String = "Start Menu, Taskbar and Action Center"
    Public Property B27 As String = "Action Center Hover and Links"
    Public Property B28 As String = "Lines, Toggles and Buttons"
    Public Property B29 As String = "Taskbar Active App Background"
    Public Property B30 As String = "Start Menu and Action Center Colors"
    Public Property B31 As String = "Taskbar Color"
    Public Property B32 As String = "Settings Icons, Text Selection, Focus Dots, Some Pressed Buttons"
    Public Property B33 As String = "Start Button Hover, Some Pressed Buttons"
    Public Property B34 As String = "Start Menu Accent Color (Maybe not effective)"
    Public Property B35 As String = "Taskbar Background (Maybe not effective)"
    Public Property B36 As String = "Start Icon Hover"
    Public Property B37 As String = "Settings Icons, Taskbar App Underline & Some Pressed Buttons"
    Public Property B38 As String = "Start Menu, Action Center, Taskbar Active App Background"
    Public Property B39 As String = "Links"
    Public Property B40 As String = "Taskbar App Underline"
    Public Property B41 As String = "Not Used"
    Public Property B42 As String = "To colorize active titlebar, please activate the toggle"
    Public Property B43 As String = "To colorize inactive titlebar, please activate the toggle"
    Public Property B44 As String = "To colorize taskbar, please activate the toggle"
    Public Property B45 As String = "To colorize start menu, action center and taskbar, please activate the toggle"
    Public Property B46 As String = "To colorize start menu and action center, please activate the toggle"
    Public Property B47 As String = "This will restart the explorer, don''t worry this won''t close other applications."
#End Region

#Region "Win32UI"
    Public Property C1 As String = "General"
    Public Property C2 As String = "App Workspace"
    Public Property C3 As String = "Background"
    Public Property C4 As String = "Desktop"
    Public Property C5 As String = "Highlight"
    Public Property C6 As String = "Highlight Text"
    Public Property C7 As String = "HotTracking Color"
    Public Property C8 As String = "Classic Buttons"
    Public Property C9 As String = "Alternate Face"
    Public Property C10 As String = "Dark Shadow"
    Public Property C11 As String = "Face"
    Public Property C12 As String = "Light"
    Public Property C13 As String = "Shadow"
    Public Property C14 As String = "Text"
    Public Property C15 As String = "Classic Titlebars"
    Public Property C16 As String = "Active Title"
    Public Property C17 As String = "Gradient Active Title"
    Public Property C18 As String = "Title Text"
    Public Property C19 As String = "Inactive Title"
    Public Property C20 As String = "Gradient Inactive Title"
    Public Property C21 As String = "Inactive Title Text"
    Public Property C22 As String = "Active Border"
    Public Property C23 As String = "Inactive Border"
    Public Property C24 As String = "Info (Tooltip)"
    Public Property C25 As String = "Window"
    Public Property C26 As String = "Menus"
    Public Property C27 As String = "Menu"
    Public Property C28 As String = "Menu Bar"
    Public Property C29 As String = "Hilight"
    Public Property C30 As String = "Frame"
    Public Property C31 As String = "Window Text"
    Public Property C32 As String = "Gray Text"
    Public Property C33 As String = "Scrollbar"
    Public Property C34 As String = "Do you remember this?"
    Public Property C35 As String = "The nearby colors are the same used in Windows 9x, some can be effective in Windows 10/11, others Not."
    Public Property C36 As String = "Load these into current palette"

#End Region

#Region "LogonUI"
    Public Property D1 As String = "LogonUI and LockScreen"
    Public Property D2 As String = "Background Color"
    Public Property D3 As String = "Personal BackColor"
    Public Property D4 As String = "Personal Accent"
    Public Property D5 As String = "Acrylic"
    Public Property D6 As String = "LogonUI Background"
    Public Property D7 As String = "Lockscreen"
#End Region

#Region "Edit Info"
    Public Property E1 As String = "Palette Name"
    Public Property E2 As String = "Palette Version:"
    Public Property E3 As String = "Palette Description:"
    Public Property E4 As String = "Author:"
    Public Property E5 As String = "Social Media Link:"
#End Region

#Region "Changelog"
    Public Property F1 As String = "Include Beta Channel"
    Public Property F2 As String = "Search for a version:"
    Public Property F3 As String = "Error reading changelog online"

    Public Property F4 As String = "Error phrasing changelog"
    Public Property F5 As String = "is not released yet, deleted or written in a wrong format."
    Public Property F6 As String = "Released on:"
#End Region

#Region "Updates"
    Public Property G1 As String = "Current Version"
    Public Property G2 As String = "New Version"
    Public Property G3 As String = "Update Size"
    Public Property G4 As String = "Release Date"
    Public Property G5 As String = "Previous Changelogs"
    Public Property G6 As String = "Update Avaliable"
    Public Property G7 As String = "Download then Close the current executable and replace it by the new update"
    Public Property G8 As String = "Save download as..."
    Public Property G9 As String = "Just Download from the browser"
    Public Property G10 As String = "Automatic Check for Updates"
    Public Property G11 As String = "Check for updates"
    Public Property G12 As String = "Checking ..."
    Public Property G13 As String = "Error reading changelog online"
    Public Property G14 As String = "Do Action"
    Public Property G15 As String = "No Avaliable Updates"
    Public Property G16 As String = "Network Error"
    Public Property G17 As String = "Error: Network issues or Github repository is private or deleted. Visit Github page for details."
    Public Property G18 As String = "Downloaded Successfully"

#End Region

#Region "About"
    Public Property H1 As String = "The following components are used to increase the functionality of the program, all rights go to their owners."
    Public Property H2 As String = "Icons by Pichon"
    Public Property H3 As String = "Color Picking Controls by Cyotek"
    Public Property H4 As String = "Image to Palette Mechanism by KSemenenko/ColorThief"
    Public Property H5 As String = "Animaton for Controls by Pavel Torgashov"
#End Region

#Region "Q/A"
    Public Property I1 As String = "Questions and Answers"
    Public Property I2 As String = "Why does Palette change colors order when I toggle (Dark\Light\Transparency\...) ?"
    Public Property I3 As String = "Because Windows stores the colors in an array in the registry. Windows Controls adapt colors from this array in dark mode in a different way to light mode, and Windows does that to assure that controls have contrast and can be seen well."
    Public Property I4 As String = "Why does coloring LogonUI not having effect in Windows 11?"
    Public Property I5 As String = "Because Microsoft restricted it by using Black colors in all logon pages."
    Public Property I6 As String = "Why is there a Win32UI palette?"
    Public Property I7 As String = "Microsoft removed this customization in recent Windows Version, but there are registry leftovers. With it you can change item selection colors and other items to keep Windows Consistant."
    Public Property I8 As String = "When I select a (Start menu\Taskbar\Action Center) color, there is no effect?"
    Public Property I9 As String = "Start Menu, Taskbar and Action Center Colors are applied when you toggle on its option."
    Public Property I10 As String = "Why does explorer restart everytime I apply a palette?"
    Public Property I11 As String = "This is done to see instant effects. You can skip restarting explorer from settings and then, to see changes you should restart explorer yourself by (TaskManager\Log Out and Relog In\Restarting Windows\...)."
    Public Property I12 As String = "Online Help\Report Issue (Github)"
    Public Property I13 As String = "Open Settings"
#End Region

#Region "Settings"
    Public Property J1 As String = "Import"
    Public Property J2 As String = "Export"
    Public Property J3 As String = "Appearance"
    Public Property J4 As String = "Dark Mode"
    Public Property J5 As String = "Light Mode"
    Public Property J6 As String = "Automatic get from current applied Windows Mode"
    Public Property J7 As String = "Automatic Updates Checking"
    Public Property J8 As String = "Updates Channel"
    Public Property J9 As String = "Theme File Type Management"
    Public Property J10 As String = "Automatic Add Extension of theme file and setting file (*.wpth,*.wpsf) to registry everytime I open the program"
    Public Property J11 As String = "Drag and Drop previews theme file in the application"
    Public Property J12 As String = "Opening theme file from explorer previews it in the application"
    Public Property J13 As String = "Opening theme file from explorer applies the theme without opening the application"
    Public Property J14 As String = "Theme Applying Behaviour"
    Public Property J15 As String = "Automatic Restart Explorer everytime I apply a theme"
    Public Property J16 As String = "Miscellaneous"
    Public Property J17 As String = "Preview and manage palettes as if my OS is:"
    Public Property J18 As String = "Deassociate Files Extensions"
    Public Property J19 As String = "Do you want to save Settings?"
    Public Property J20 As String = "Are you sure from removing files association (*.wpth, *.wpsf) from registry?"
    Public Property J21 As String = "Note: You can reassociate them by activating its checkbox and restarting the application."
    Public Property J22 As String = "Are you sure from Uninstalling the program?"
    Public Property J23 As String = "This will delete associated files extensions from registry and the application itself."
    Public Property J24 As String = "It''s Recommended. Don''t worry it won''t close your work. If you are obsessed about this, save your work at first."

#End Region

#Region "Edit Info"
    Public Property K1 As String = "Palette Name"
    Public Property K2 As String = "Palette Version:"
    Public Property K3 As String = "Palette Description:"
    Public Property K4 As String = "Author:"
    Public Property K5 As String = "Social Media Link:"
    Public Property K6 As String = "You can''t leave Palette Name Empty. Please type a name to it."
    Public Property K7 As String = "You can''t leave Author''s Name Empty. Please type Author''s name or your name."
    Public Property K8 As String = "You can''t leave Palette Version Empty. Please type a version to it in this style (x.x.x.x), replacing (x) by numbers"
    Public Property K9 As String = "Wrong Version Fomrat. Please type the version to it in this style (x.x.x.x), replacing (x) by numbers."
    Public Property K10 As String = "Load these info"
#End Region

#Region "Complex Save"
    Public Property L1 As String = "Current Palette Changed. Choose what you want:"
    Public Property L2 As String = "Saving Mode"
    Public Property L3 As String = "Save theme palette file"
    Public Property L4 As String = "Save theme palette file as ..."
    Public Property L5 As String = "Don't save"
    Public Property L6 As String = "Applying Mode"
    Public Property L7 As String = "Apply the theme palette"
#End Region

#Region "Color Picker"
    Public Property M1 As String = "Methods"
    Public Property M2 As String = "Get Palette from Image"
    Public Property M3 As String = "Color Grid"
    Public Property M4 As String = "Color Wheel"
    Public Property M5 As String = "Select a mode"
    Public Property M6 As String = "Current Wallpaper"
    Public Property M7 As String = "Browse for an image"
    Public Property M8 As String = "Options"
    Public Property M9 As String = "Maximum Colors"
    Public Property M10 As String = "Quality"
    Public Property M11 As String = "Ignore White Colors"
    Public Property M12 As String = "Palette"
    Public Property M13 As String = "Extract"
    Public Property M14 As String = "Drag me and release to pick a color"
    Public Property M15 As String = "Select"
    Public Property M16 As String = "Extracting palette from image depends on your device''s performance, maximum palette colors number, image quality and its resolution ..."
    Public Property M17 As String = "Sorting Colors in Palette ..."
#End Region


    Sub New()

    End Sub

    Sub LoadLanguage(ByVal File As String)
        Dim type1 As Type = [GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()
        Dim LS As New List(Of String)
        LS.Clear()
        CList_FromStr(LS, IO.File.ReadAllText(File))

        For Each X As String In LS
            Dim Y As String = X.Split("=")(0)
            Dim Z As String = X.Split("=")(1).TrimStart

            For Each [property] As PropertyInfo In properties1
                If [property].Name = Y Then

                    [property].SetValue(Me, Convert.ChangeType(Z, [property].PropertyType), Nothing)
                End If
            Next

        Next

    End Sub

    Sub ApplyLanguage()

    End Sub

    Sub ExportLanguage(ByVal File As String)
        Dim type1 As Type = [GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()
        Dim LS As New List(Of String)
        LS.Clear()

        For Each [property] As PropertyInfo In properties1
            LS.Add([property].Name & "= " & [property].GetValue(Me, Nothing))
        Next

        IO.File.WriteAllText(File, CStr_FromList(LS))

    End Sub

End Class
