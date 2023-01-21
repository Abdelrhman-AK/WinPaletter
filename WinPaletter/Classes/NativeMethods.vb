Imports System.Runtime.InteropServices
Imports System.Text

Namespace NativeMethods
    Public Class Dwmapi

        <DllImport("dwmapi.dll", EntryPoint:="#131", PreserveSig:=False)>
        Public Shared Sub DwmSetColorizationParameters(ByRef parameters As DWM_COLORIZATION_PARAMS, ByVal unknown As Boolean)
        End Sub

        <DllImport("dwmapi.dll")>
        Public Shared Sub DwmEnableComposition(ByVal uCompositionAction As CompositionAction)
        End Sub

        <DllImport("dwmapi.dll")>
        Public Shared Function DwmIsCompositionEnabled(ByRef enabled As Boolean) As Integer
        End Function

        <DllImport("dwmapi.dll")>
        Public Shared Function DwmEnableBlurBehindWindow(ByVal hWnd As IntPtr, ByRef pBlurBehind As DwmBlurbehind) As Integer
        End Function

        <DllImport("dwmapi")> Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarInset As MARGINS) As Integer
        End Function

        <DllImport("dwmapi")> Friend Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
        End Function

        <DllImport("dwmapi.dll")> Friend Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal dwAttribute As DWMATTRIB, ByRef pvAttribute As Integer, ByVal cbAttribute As Integer) As Integer
        End Function

        Public Const CS_DROPSHADOW As Integer = &H20000
        Public Const WM_NCPAINT As Integer = &H85

        Public Enum DWMATTRIB
            DWMWA_SYSTEMBACKDROP_TYPE = 38
            DWMWA_MICA_EFFECT = 1029
        End Enum

        Public Structure DwmBlurbehind
            Public DwFlags As Integer
            Public FEnable As Boolean
            Public HRgnBlur As IntPtr
            Public FTransitionOnMaximized As Boolean
        End Structure

        Public Shared Sub DropMica(ByVal frm As Form, ByVal yes As Boolean)
            Dim extend As Boolean = XenonCore.GetDarkMode

            If Environment.OSVersion.Version.Build >= 22523 Then
                Dim micaValue As Integer = &H4 '&H2
                Dim tabbedvalue As Integer = &H4

                If extend Then
                    DwmSetWindowAttribute(frm.Handle, DWMATTRIB.DWMWA_SYSTEMBACKDROP_TYPE, micaValue, Marshal.SizeOf(GetType(Integer)))
                Else
                    DwmSetWindowAttribute(frm.Handle, DWMATTRIB.DWMWA_SYSTEMBACKDROP_TYPE, tabbedvalue, Marshal.SizeOf(GetType(Integer)))
                End If

            Else
                Dim trueValue As Integer = &H1
                DwmSetWindowAttribute(frm.Handle, DWMATTRIB.DWMWA_MICA_EFFECT, trueValue, Marshal.SizeOf(GetType(Integer)))
            End If

            Dim DarkMode As Boolean = XenonCore.GetDarkMode
            Dim m As New MARGINS()

            If yes Then
                DwmExtendFrameIntoClientArea(frm.Handle, m)
            Else
                Dim mar As New MARGINS With {
                    .bottomHeight = 0,
                    .leftWidth = 0,
                    .rightWidth = 0,
                    .topHeight = 0
                }
                DwmExtendFrameIntoClientArea(frm.Handle, mar)
            End If
        End Sub

        Public Enum CompositionAction As Integer
            DWM_EC_DISABLECOMPOSITION = 0
            DWM_EC_ENABLECOMPOSITION = 1
        End Enum

        <StructLayout(LayoutKind.Sequential)> Public Structure MARGINS
            Public leftWidth As Integer
            Public rightWidth As Integer
            Public topHeight As Integer
            Public bottomHeight As Integer
        End Structure


        Public Structure DWM_COLORIZATION_PARAMS
            Public clrColor As Integer
            Public clrAfterGlow As Integer
            Public nIntensity As Integer
            Public clrAfterGlowBalance As Integer
            Public clrBlurBalance As Integer
            Public clrGlassReflectionIntensity As Integer
            Public fOpaque As Boolean
        End Structure

    End Class

    Public Class User32
        <DllImport("user32.dll")>
        Public Shared Function LoadCursor(ByVal hInstance As Integer, ByVal lpCursorName As Integer) As Integer
        End Function
        <DllImport("user32.dll")>
        Public Shared Function SetCursor(ByVal hCursor As Integer) As Integer
        End Function

        <DllImport("user32.dll")>
        Public Shared Function AnimateWindow(ByVal hWnd As IntPtr, ByVal time As Integer, ByVal flags As AnimateWindowFlags) As Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Public Shared Function SendMessageTimeout(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As UIntPtr, ByVal lParam As IntPtr, ByVal fuFlags As SendMessageTimeoutFlags, ByVal uTimeout As UInteger, <Out> ByRef lpdwResult As UIntPtr) As IntPtr
        End Function

        Declare Function SHChangeNotify Lib "Shell32.dll" (ByVal wEventID As Int32, ByVal uFlags As Int32, ByVal dwItem1 As Int32, ByVal dwItem2 As Int32) As Int32

        Friend Declare Function SetWindowCompositionAttribute Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef data As WindowCompositionAttributeData) As Integer

        Public Declare Auto Function FindWindow Lib "user32.dll" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr

        <DllImport("user32.dll")>
        Public Shared Function SetSystemCursor(ByVal hcur As IntPtr, ByVal id As Integer) As Boolean
        End Function

        Declare Function LoadCursorFromFile Lib "user32.dll" Alias "LoadCursorFromFileA" (ByVal lpFileName As String) As IntPtr

        <DllImport("Shell32.dll", EntryPoint:="SHDefExtractIconW")>
        Public Shared Function SHDefExtractIconW(<MarshalAs(UnmanagedType.LPTStr)> ByVal pszIconFile As String, ByVal iIndex As Integer, ByVal uFlags As UInteger, ByRef phiconLarge As IntPtr, ByRef phiconSmall As IntPtr, ByVal nIconSize As UInteger) As Integer
        End Function

        <DllImport("user32.dll", EntryPoint:="DestroyIcon")>
        Public Shared Function DestroyIcon(ByVal hIcon As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("user32.dll")>
        Public Shared Function SetSysColors(ByVal cElements As Integer, ByVal lpaElements As Integer(), ByVal lpaRgbValues As UInteger()) As Boolean
        End Function

        Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, ByVal lpvParam As Integer, fuWinIni As Integer) As Integer

        Public Shared Function MAKEICONSIZE(ByVal low As Integer, ByVal high As Integer) As Integer
            Return (high << 16) Or (low And &HFFFF)
        End Function

        Public Shared Function ExtractSmallIcon(Path As String, Optional IconIndex As Integer = 0)
            Dim ico As Icon = Nothing
            'Make the nIconSize value (See the Msdn documents). The LOWORD is the Large Icon Size. The HIWORD is the Small Icon Size.
            'The largest size for an icon is 256.
            Dim LargeAndSmallSize As UInteger = CUInt(MAKEICONSIZE(256, 16))

            Dim hLrgIcon As IntPtr = IntPtr.Zero
            Dim hSmlIcon As IntPtr = IntPtr.Zero

            Dim result As Integer = SHDefExtractIconW(Path, IconIndex, 0, hLrgIcon, hSmlIcon, LargeAndSmallSize)

            If result = 0 Then
                If ico IsNot Nothing Then ico.Dispose()

                'if the large and/or small icons where created in the unmanaged memory successfuly then create
                'a clone of them in the managed icons and delete the icons in the unmanaged memory.

                If hSmlIcon <> IntPtr.Zero Then
                    ico = CType(Icon.FromHandle(hSmlIcon).Clone, Icon)
                    DestroyIcon(hSmlIcon)
                End If

            End If

            Return ico
        End Function
        Enum OCR_SYSTEM_CURSORS As Integer

            ' Standard arrow And small hourglass
            OCR_APPSTARTING = 32650

            'Standard arrow
            OCR_NORMAL = 32512

            'Crosshair
            OCR_CROSS = 32515

            'Windows 2000/XP: Hand
            OCR_HAND = 32649

            'Arrow And question mark
            OCR_HELP = 32651

            'I-beam
            OCR_IBEAM = 32513

            'Slashed circle
            OCR_NO = 32648

            'Four-pointed arrow pointing north south east And west
            OCR_SIZEALL = 32646

            'Double-pointed arrow pointing northeast And southwest
            OCR_SIZENESW = 32643

            'Double-pointed arrow pointing north And south
            OCR_SIZENS = 32645

            'Double-pointed arrow pointing northwest And southeast
            OCR_SIZENWSE = 32642

            'Double-pointed arrow pointing west And east
            OCR_SIZEWE = 32644

            'Vertical arrow
            OCR_UP = 32516

            'Hourglass
            OCR_WAIT = 32514
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Friend Structure AccentPolicy
            Public AccentState As AccentState
            Public AccentFlags As Integer
            Public GradientColor As Integer
            Public AnimationId As Integer
        End Structure

        Friend Structure WindowCompositionAttributeData
            Public Attribute As WindowCompositionAttribute
            Public Data As IntPtr
            Public SizeOfData As Integer
        End Structure

        Public Enum WindowCompositionAttribute
            WCA_ACCENT_POLICY = 19
            WCA_USEDARKMODECOLORS = 26
        End Enum

        Friend Enum AccentState
            ACCENT_DISABLED = 0
            ACCENT_ENABLE_GRADIENT = 1
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2
            ACCENT_ENABLE_BLURBEHIND = 3
            ACCENT_ENABLE_TRANSPARANT = 6
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Public Structure WINDOWCOMPOSITIONATTRIBDATA
            Public Attrib As WindowCompositionAttribute
            Public pvData As IntPtr
            Public cbData As Integer
        End Structure

        Public Shared Sub DarkTitlebar(ByVal hWnd As IntPtr, DarkMode As Boolean)

            If Dwmapi.DwmSetWindowAttribute(hWnd, 19, If(DarkMode, 1, 0), 4) <> 0 Then Dwmapi.DwmSetWindowAttribute(hWnd, 20, If(DarkMode, 1, 0), 4)

            'Exit Sub

            'If IsWindows10OrGreater(18362) Then
            'SetProp(hWnd, "UseImmersiveDarkModeColors", New IntPtr(If(DarkMode, 1, 0)))
            'Else
            'Dim size As Integer = Marshal.SizeOf(DarkMode)
            'Dim ptr As IntPtr = Marshal.AllocHGlobal(size)
            'Marshal.StructureToPtr(DarkMode, ptr, False)
            'Dim data As WindowCompositionAttributeData = New WindowCompositionAttributeData With {
            '.Attribute = WindowCompositionAttribute.WCA_USEDARKMODECOLORS,
            '.Data = ptr,
            '.SizeOfData = size
            '}
            'SetWindowCompositionAttribute(hWnd, data)
            'End If

        End Sub

        Enum SendMessageTimeoutFlags As UInteger
            SMTO_NORMAL = &H0
            SMTO_BLOCK = &H1
            SMTO_ABORTIFHUNG = &H2
            SMTO_NOTIMEOUTIFNOTHUNG = &H8
        End Enum

        Public Shared WM_DWMCOLORIZATIONCOLORCHANGED As Integer = &H320
        Public Shared WM_DWMCOMPOSITIONCHANGED As Integer = &H31E
        Public Shared WM_THEMECHANGED As Integer = &H31A
        Public Shared WM_SYSCOLORCHANGE As Integer = &H15
        Public Shared WM_PALETTECHANGED As Integer = &H311
        Public Shared WM_WININICHANGE As UInteger = &H1A
        Public Shared WM_SETTINGCHANGE As UInteger = WM_WININICHANGE
        Public Shared HWND_MESSAGE As Int32 = -&H3
        Public Shared HWND_BROADCAST As New IntPtr(&HFFFF)
        Public Shared MSG_TIMEOUT As Integer = 5000
        Public Shared RESULT As UIntPtr

        <Flags>
        Enum AnimateWindowFlags
            AW_HOR_POSITIVE = &H0
            AW_HOR_NEGATIVE = &H2
            AW_VER_POSITIVE = &H4
            AW_VER_NEGATIVE = &H8
            AW_CENTER = &H10
            AW_HIDE = &H10000
            AW_ACTIVATE = &H20000
            AW_SLIDE = &H40000
            AW_BLEND = &H80000
        End Enum

    End Class

    Public Class Kernel32
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function LoadLibraryEx(ByVal lpFileName As String, ByVal hFile As IntPtr, ByVal dwFlags As UInteger) As IntPtr
        End Function
        <DllImport("Kernel32.dll", EntryPoint:="LockResource")>
        Public Shared Function LockResource(ByVal hGlobal As IntPtr) As IntPtr
        End Function
        <DllImport("kernel32.dll")>
        Public Shared Function FindResource(ByVal hModule As IntPtr, ByVal lpID As Integer, ByVal lpType As String) As IntPtr
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function LoadResource(ByVal hModule As IntPtr, ByVal hResInfo As IntPtr) As IntPtr
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function SizeofResource(ByVal hModule As IntPtr, ByVal hResInfo As IntPtr) As UInteger
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function Wow64DisableWow64FsRedirection(ByRef ptr As IntPtr) As Boolean
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function Wow64RevertWow64FsRedirection(ByVal ptr As IntPtr) As Boolean
        End Function

    End Class

    Public Class Uxtheme
        <DllImport("UxTheme.DLL", BestFitMapping:=False, CallingConvention:=CallingConvention.Winapi, CharSet:=CharSet.Unicode, EntryPoint:="#65")>
        Public Shared Function SetSystemVisualStyle(ByVal pszFilename As String, ByVal pszColor As String, ByVal pszSize As String, ByVal dwReserved As Integer) As Integer
        End Function

        <DllImport("uxtheme", ExactSpelling:=True)>
        Public Shared Function EnableTheming(ByVal fEnable As Integer) As Integer
        End Function

        Public Declare Unicode Function GetCurrentThemeName Lib "uxtheme" (ByVal stringThemeName As StringBuilder, ByVal lengthThemeName As Integer, ByVal stringColorName As StringBuilder, ByVal lengthColorName As Integer, ByVal stringSizeName As StringBuilder, ByVal lengthSizeName As Integer) As Int32

        <DllImport("uxtheme.dll", EntryPoint:="#135")>
        Friend Shared Function SetPreferredAppMode(ByVal appMode As PreferredAppMode) As Integer
        End Function

        <DllImport("uxtheme.dll", ExactSpelling:=True, CharSet:=CharSet.Unicode)>
        Public Shared Function SetWindowTheme(ByVal hwnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
        End Function

        Friend Enum PreferredAppMode
            [Default]
            AllowDark
            ForceDark
            ForceLight
            Max
        End Enum

        <DllImport("uxtheme.dll", EntryPoint:="#135")>
        Friend Shared Function AllowDarkModeForApp(ByVal allow As Boolean) As Integer
        End Function

        <DllImport("uxtheme.dll", EntryPoint:="#133")>
        Friend Shared Function AllowDarkModeForWindow(ByVal handle As IntPtr, ByVal allow As Boolean) As Integer
        End Function
    End Class

    Public Class Shell32
        Public Const MAX_PATH As Integer = 260

        <Flags>
        Public Enum SHGSI
            ICONLOCATION = 0
            ICON = &H100
            SYSICONINDEX = &H4000
            LINKOVERLAY = &H8000
            SELECTED = &H10000
            LARGEICON = &H0
            SMALLICON = &H1
            SHELLICONSIZE = &H4
        End Enum

        Public Enum SHSTOCKICONID
            ''' <summary>
            ''' Blank document icon (Document Of a type With no associated application).
            ''' </summary>
            DOCNOASSOC = 0

            ''' <summary>
            ''' Application-associated document icon (Document Of a type With an associated application).
            ''' </summary>
            DOCASSOC = 1

            ''' <summary>
            ''' Generic application With no custom icon.
            ''' </summary>
            APPLICATION = 2

            ''' <summary>
            ''' Folder (generic unspecified state).
            ''' </summary>
            FOLDER = 3

            ''' <summary>
            ''' Folder (open).
            ''' </summary>
            FOLDEROPEN = 4

            ''' <summary>
            ''' 5.25-inch disk drive.
            ''' </summary>
            DRIVE525 = 5

            ''' <summary>
            ''' 3.5-inch disk drive.
            ''' </summary>
            DRIVE35 = 6

            ''' <summary>
            ''' Removable drive.
            ''' </summary>
            DRIVEREMOVE = 7

            ''' <summary>
            ''' Fixed drive (hard disk).
            ''' </summary>
            DRIVEFIXED = 8

            ''' <summary>
            ''' Network drive (connected).
            ''' </summary>
            DRIVENET = 9

            ''' <summary>
            ''' Network drive (disconnected).
            ''' </summary>
            DRIVENETDISABLED = 10

            ''' <summary>
            ''' CD drive.
            ''' </summary>
            DRIVECD = 11

            ''' <summary>
            ''' RAM disk drive.
            ''' </summary>
            DRIVERAM = 12

            ''' <summary>
            ''' The entire network.
            ''' </summary>
            WORLD = 13

            ''' <summary>
            ''' A computer On the network.
            ''' </summary>
            SERVER = 15

            ''' <summary>
            ''' A local printer Or print destination.
            ''' </summary>
            PRINTER = 16

            ''' <summary>
            ''' The Network virtual folder (FOLDERID_NetworkFolder/CSIDL_NETWORK).
            ''' </summary>
            MYNETWORK = 17

            ''' <summary>
            ''' The Search feature.
            ''' </summary>
            FIND = 22

            ''' <summary>
            ''' The Help And Support feature.
            ''' </summary>
            HELP = 23

            ''' <summary>
            ''' Overlay For a Shared item.
            ''' </summary>
            SHARE = 28

            ''' <summary>
            ''' Overlay For a shortcut.
            ''' </summary>
            LINK = 29

            ''' <summary>
            ''' Overlay For items that are expected To be slow To access.
            ''' </summary>
            SLOWFILE = 30

            ''' <summary>
            ''' The Recycle Bin (empty).
            ''' </summary>
            RECYCLER = 31

            ''' <summary>
            ''' The Recycle Bin (Not empty).
            ''' </summary>
            RECYCLERFULL = 32

            ''' <summary>
            ''' Audio CD media.
            ''' </summary>
            MEDIACDAUDIO = 40

            ''' <summary>
            ''' Security lock.
            ''' </summary>
            LOCK = 47

            ''' <summary>
            ''' A virtual folder that contains the results Of a search.
            ''' </summary>
            AUTOLIST = 49

            ''' <summary>
            ''' A network printer.
            ''' </summary>
            PRINTERNET = 50

            ''' <summary>
            ''' A server Shared On a network.
            ''' </summary>
            SERVERSHARE = 51

            ''' <summary>
            ''' A local fax printer.
            ''' </summary>
            PRINTERFAX = 52

            ''' <summary>
            ''' A network fax printer.
            ''' </summary>
            PRINTERFAXNET = 53

            ''' <summary>
            ''' A file that receives the output Of a Print To file operation.
            ''' </summary>
            PRINTERFILE = 54

            ''' <summary>
            ''' A category that results from a Stack by command To organize the contents Of a folder.
            ''' </summary>
            STACK = 55

            ''' <summary>
            ''' Super Video CD (SVCD) media.
            ''' </summary>
            MEDIASVCD = 56

            ''' <summary>
            ''' A folder that contains only subfolders As child items.
            ''' </summary>
            STUFFEDFOLDER = 57

            ''' <summary>
            ''' Unknown drive type.
            ''' </summary>
            DRIVEUNKNOWN = 58

            ''' <summary>
            ''' DVD drive.
            ''' </summary>
            DRIVEDVD = 59

            ''' <summary>
            ''' DVD media.
            ''' </summary>
            MEDIADVD = 60

            ''' <summary>
            ''' DVD-RAM media.
            ''' </summary>
            MEDIADVDRAM = 61

            ''' <summary>
            ''' DVD-RW media.
            ''' </summary>
            MEDIADVDRW = 62

            ''' <summary>
            ''' DVD-R media.
            ''' </summary>
            MEDIADVDR = 63

            ''' <summary>
            ''' DVD-ROM media.
            ''' </summary>
            MEDIADVDROM = 64

            ''' <summary>
            ''' CD+ (enhanced audio CD) media.
            ''' </summary>
            MEDIACDAUDIOPLUS = 65

            ''' <summary>
            ''' CD-RW media.
            ''' </summary>
            MEDIACDRW = 66

            ''' <summary>
            ''' CD-R media.
            ''' </summary>
            MEDIACDR = 67

            ''' <summary>
            ''' A writeable CD In the process Of being burned.
            ''' </summary>
            MEDIACDBURN = 68

            ''' <summary>
            ''' Blank writable CD media.
            ''' </summary>
            MEDIABLANKCD = 69

            ''' <summary>
            ''' CD-ROM media.
            ''' </summary>
            MEDIACDROM = 70

            ''' <summary>
            ''' An audio file.
            ''' </summary>
            AUDIOFILES = 71

            ''' <summary>
            ''' An image file.
            ''' </summary>
            IMAGEFILES = 72

            ''' <summary>
            ''' A video file.
            ''' </summary>
            VIDEOFILES = 73

            ''' <summary>
            ''' A mixed file.
            ''' </summary>
            MIXEDFILES = 74

            ''' <summary>
            ''' Folder back.
            ''' </summary>
            FOLDERBACK = 75

            ''' <summary>
            ''' Folder front.
            ''' </summary>
            FOLDERFRONT = 76

            ''' <summary>
            ''' Security shield. Use For UAC prompts only.
            ''' </summary>
            SHIELD = 77

            ''' <summary>
            ''' Warning.
            ''' </summary>
            WARNING = 78

            ''' <summary>
            ''' Informational.
            ''' </summary>
            INFO = 79

            ''' <summary>
            ''' Error.
            ''' </summary>
            [Error] = 80

            ''' <summary>
            ''' Key.
            ''' </summary>
            KEY = 81

            ''' <summary>
            ''' Software.
            ''' </summary>
            SOFTWARE = 82

            ''' <summary>
            ''' A UI item such As a button that issues a rename command.
            ''' </summary>
            RENAME = 83

            ''' <summary>
            ''' A UI item such As a button that issues a delete command.
            ''' </summary>
            DELETE = 84

            ''' <summary>
            ''' Audio DVD media.
            ''' </summary>
            MEDIAAUDIODVD = 85

            ''' <summary>
            ''' Movie DVD media.
            ''' </summary>
            MEDIAMOVIEDVD = 86

            ''' <summary>
            ''' Enhanced CD media.
            ''' </summary>
            MEDIAENHANCEDCD = 87

            ''' <summary>
            ''' Enhanced DVD media.
            ''' </summary>
            MEDIAENHANCEDDVD = 88

            ''' <summary>
            ''' High definition DVD media In the HD DVD format.
            ''' </summary>
            MEDIAHDDVD = 89

            ''' <summary>
            ''' High definition DVD media In the Blu-ray Disc™ format.
            ''' </summary>
            MEDIABLURAY = 90

            ''' <summary>
            ''' Video CD (VCD) media.
            ''' </summary>
            MEDIAVCD = 91

            ''' <summary>
            ''' DVD+R media.
            ''' </summary>
            MEDIADVDPLUSR = 92

            ''' <summary>
            ''' DVD+RW media.
            ''' </summary>
            MEDIADVDPLUSRW = 93

            ''' <summary>
            ''' A desktop computer.
            ''' </summary>
            DESKTOPPC = 94

            ''' <summary>
            ''' A mobile computer (laptop).
            ''' </summary>
            MOBILEPC = 95

            ''' <summary>
            ''' The User Accounts Control Panel item.
            ''' </summary>
            USERS = 96

            ''' <summary>
            ''' Smart media.
            ''' </summary>
            MEDIASMARTMEDIA = 97

            ''' <summary>
            ''' CompactFlash media.
            ''' </summary>
            MEDIACOMPACTFLASH = 98

            ''' <summary>
            ''' A cell phone.
            ''' </summary>
            DEVICECELLPHONE = 99

            ''' <summary>
            ''' A digital camera.
            ''' </summary>
            DEVICECAMERA = 100

            ''' <summary>
            ''' A digital video camera.
            ''' </summary>
            DEVICEVIDEOCAMERA = 101

            ''' <summary>
            ''' An audio player.
            ''' </summary>
            DEVICEAUDIOPLAYER = 102

            ''' <summary>
            ''' Connect To network.
            ''' </summary>
            NETWORKCONNECT = 103

            ''' <summary>
            ''' The Network And Internet Control Panel item.
            ''' </summary>
            INTERNET = 104

            ''' <summary>
            ''' A compressed file With a .zip file name extension.
            ''' </summary>
            ZIPFILE = 105

            ''' <summary>
            ''' The Additional Options Control Panel item.
            ''' </summary>
            SETTINGS = 106

            ''' <summary>
            ''' High definition DVD drive (any type - HD DVD-ROM HD DVD-R HD-DVD-RAM) that uses the HD DVD format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            DRIVEHDDVD = 132

            ''' <summary>
            ''' High definition DVD drive (any type - BD-ROM BD-R BD-RE) that uses the Blu-ray Disc format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            DRIVEBD = 133

            ''' <summary>
            ''' High definition DVD-ROM media In the HD DVD-ROM format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIAHDDVDROM = 134

            ''' <summary>
            ''' High definition DVD-R media In the HD DVD-R format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIAHDDVDR = 135

            ''' <summary>
            ''' High definition DVD-RAM media In the HD DVD-RAM format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIAHDDVDRAM = 136

            ''' <summary>
            ''' High definition DVD-ROM media In the Blu-ray Disc BD-ROM format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIABDROM = 137

            ''' <summary>
            ''' High definition write-once media In the Blu-ray Disc BD-R format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIABDR = 138

            ''' <summary>
            ''' High definition read/write media In the Blu-ray Disc BD-RE format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIABDRE = 139

            ''' <summary>
            ''' A cluster disk array.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            CLUSTEREDDRIVE = 140

            ''' <summary>
            ''' The highest valid value In the enumeration. Values over 160 are Windows 7-only icons.
            ''' </summary>
            MAX_ICONS = 174
        End Enum

        <DllImport("shell32.dll")> Shared Sub SHChangeNotify(ByVal wEventId As Integer, ByVal uFlags As Integer, ByVal dwItem1 As Integer, ByVal dwItem2 As Integer)
        End Sub

        Public Const SHCNE_ASSOCCHANGED = &H8000000
        Public Const SHCNF_IDLIST = 0

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
        Public Structure SHSTOCKICONINFO
            Public cbSize As UInt32
            Public hIcon As IntPtr
            Public iSysIconIndex As Int32
            Public iIcon As Int32
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)>
            Public szPath As String
        End Structure

        <DllImport("Shell32.dll", SetLastError:=False)>
        Public Shared Function SHGetStockIconInfo(ByVal siid As SHSTOCKICONID, ByVal uFlags As SHGSI, ByRef psii As SHSTOCKICONINFO) As Int32

        End Function

        Public Shared Function GetSystemIcon(_Icon As SHSTOCKICONID, _Type As SHGSI) As Icon
            Dim sii As New SHSTOCKICONINFO With {.cbSize = Marshal.SizeOf(GetType(SHSTOCKICONINFO))}
            SHGetStockIconInfo(_Icon, _Type, sii)
            Return Icon.FromHandle(sii.hIcon)
        End Function
    End Class
End Namespace
