﻿using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows effects and animations
    /// </summary>
    public struct WinEffects : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled;

        /// <summary>Windows animation (open app, close, minimize, maximize, ...)</summary>
        public bool WindowAnimation;

        /// <summary>Shadow arround window border</summary>
        public bool WindowShadow;

        /// <summary>Controls all Windows effects, including WindowAnimation and WindowShadow</summary>
        public bool WindowUIEffects;

        /// <summary>Show contents of a window while dragging</summary>
        public bool ShowWinContentDrag;

        /// <summary>Enable animation for controls inside window</summary>
        public bool AnimateControlsInsideWindow;

        /// <summary>Enable menu show animation</summary>
        public bool MenuAnimation;

        /// <summary>contextMenu animation type. It can be fade or scroll</summary>
        public MenuAnimType MenuFade;

        /// <summary>Fade selection after clicking on a menu item</summary>
        public bool MenuSelectionFade;

        /// <summary>
        /// Delay menu show in milliseconds
        /// <br><b>It is an unsigned integer (as User32.SystemParameterInfo requires DWORD not INT)</b></br>
        /// </summary>
        public uint MenuShowDelay;

        /// <summary>Animate combo box control</summary>
        public bool ComboBoxAnimation;

        /// <summary>Smooth scrolling animation for list box</summary>
        public bool ListBoxSmoothScrolling;

        /// <summary>Enable tooltip animation</summary>
        public bool TooltipAnimation;

        /// <summary>ToolTip appearance animation. It can be fade or scroll</summary>
        public MenuAnimType TooltipFade;

        /// <summary>Show shadow in icons labels at desktop</summary>
        public bool IconsShadow;

        /// <summary>
        /// - If true, desktop icons selection will be colored transparent rectangle
        /// <br></br>- If false, desktop icons selection will be rectangle with dotted border
        /// </summary>
        public bool IconsDesktopTranslSel;

        /// <summary>
        /// Make every menu item has an underline to inform the user that this menu item can be triggered by clicking on ALT+LETTER.
        /// <br>For example: when menu item 'EȢit' with letter 'x' has an underline, clicking on ALT+X will trigger this menu item.</br>
        /// <br>If false, 'EȢit' will be 'Exit'.</br>
        /// </summary>
        public bool KeyboardUnderline;

        /// <summary>
        /// Width of dotted rectangle on focused classic button
        /// <br><b>It is an unsigned integer (as User32.SystemParameterInfo requires DWORD not INT)</b></br>
        /// </summary>
        public uint FocusRectWidth;

        /// <summary>
        /// Height of dotted rectangle on focused classic button
        /// <br><b>It is an unsigned integer (as User32.SystemParameterInfo requires DWORD not INT)</b></br>
        /// </summary>
        public uint FocusRectHeight;

        /// <summary>
        /// Width of text cursor (carret)
        /// <br></br> For example
        /// <br></br>   1: Hello world! ▓
        /// <br></br>   5: Hello world! ▓▓▓▓▓
        /// </summary>
        public uint Caret;

        /// <summary>Show ballon or notification for milliseconds</summary>
        public int NotificationDuration;

        /// <summary>
        /// Shake a window from its titlebar to minimize rest windows
        /// <br></br><b>- Targets Windows 7 and later</b>
        /// </summary>
        public bool ShakeToMinimize;

        /// <summary>Enable active window tracking feature</summary>
        public bool AWT_Enabled;

        /// <summary>Active window tracking: bring window to top when mouse enters it</summary>
        public bool AWT_BringActivatedWindowToTop;

        /// <summary>Active window tracking: delay bringing window to top for milliseconds</summary>
        public int AWT_Delay;

        /// <summary>Move cusror to the default button when a window or a dialog appears</summary>
        public bool SnapCursorToDefButton;

        /// <summary>
        /// Enable classic context menus for Windows 11
        /// <br></br><b>- Requires Explorer restart</b>
        /// </summary>
        public bool Win11ClassicContextMenu;

        /// <summary>
        /// Make Windows Explorer shows items in SysListView32 style (that looks like Windows XP and Vista) in higher editions of Windows
        /// <br></br><b>- Targets Windows 7 and later</b>
        /// </summary>
        public bool SysListView32;

        /// <summary>
        /// Show seconds in taskbar clock
        /// <br></br><b>- Targets Windows 10, and 11 with Moment 3 update</b>
        /// </summary>
        public bool ShowSecondsInSystemClock;

        /// <summary>
        /// Replace rectangle notifications by classic ballons
        /// <br></br><b>- Targets Windows 8 and 8.1</b>
        /// </summary>
        public bool BalloonNotifications;

        /// <summary>Paint Windows edition on desktop</summary>
        public bool PaintDesktopVersion;

        /// <summary>Replace Windows 11 boot solid circle by spinning dots (of Windows 8/8.1/10)</summary>
        public bool Win11BootDots;

        /// <summary>
        /// Controls Windows Explorer bar/ribbon
        /// <br></br><b>- It is be better to be modified with ExplorerPatcher installed</b>
        /// </summary>
        public ExplorerBar Win11ExplorerBar;

        /// <summary>Disable navigation bar in open\save dialogs. Requires ExplorerPatcher</summary>
        public bool DisableNavBar;

        /// <summary>Automatically hide scroll bars in modern apps (UWP/WinUI3) in Windows 10/11</summary>
        public bool AutoHideScrollBars;

        /// <summary>
        /// Full screen start menu
        /// <br></br><b>- Targets Windows 10</b>
        /// </summary>
        public bool FullScreenStartMenu;

        /// <summary>Enable accessibility feature: color filter</summary>
        public bool ColorFilter_Enabled;

        /// <summary>Color filter type</summary>
        public ColorFilters ColorFilter;

        /// <summary>
        /// Enable classic volume mixer
        /// <br></br><b>- Targets Windows 10</b>
        /// </summary>
        public bool ClassicVolMixer;

        /// <summary>
        /// Enable aero peek feature: hovering on taskbar right corner will show apps with aero transparent glass rectangles on desktop.
        /// </summary>
        public bool EnableAeroPeek;

        ///
        public bool AlwaysHibernateThumbnails;

        /// <summary>
        /// Enumeration for Windows Explorer bar types
        /// </summary>
        public enum ExplorerBar
        {
            /// <summary>Restore to default type according to Windows edition.</summary>
            Default,
            /// <summary>
            /// Ribbon. May require ExplorerPatcher in Windows 11.
            /// <br>It is the default type in Windows 8/8.1/10</br>
            /// </summary>
            Ribbon,
            /// <summary>
            /// Bar. May require ExplorerPatcher in Windows 11.
            /// <br>It is the default type in Windows Vista/7</br>
            /// </summary>
            Bar
        }

        /// <summary>
        /// Enumeration for accessibility feature: color filters
        /// </summary>
        public enum ColorFilters
        {
            ///
            Grayscale,
            ///
            Inverted,
            ///
            GrayscaleInverted,
            ///
            RedGreen_deuteranopia,
            ///
            RedGreen_protanopia,
            ///
            BlueYellow
        }

        /// <summary>
        /// Enumeration for menu animation types
        /// </summary>
        public enum MenuAnimType
        {
            /// <summary>Used in modern versions of Windows</summary>
            Fade,
            /// <summary>Used in classic versions of Windows (Windows 9x)</summary>
            Scroll
        }

        /// <summary>
        /// Loads WinEffects data from registry
        /// </summary>
        /// <param name="default">Default WinEffects data structure</param>
        public void Load(WinEffects @default)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", string.Empty, true));

            if (!SystemParametersInfo(SPI.SPI_GETDROPSHADOW, 0, ref WindowShadow, SPIF.SPIF_NONE))
                WindowShadow = @default.WindowShadow;

            if (!SystemParametersInfo(SPI.SPI_GETUIEFFECTS, 0, ref WindowUIEffects, SPIF.SPIF_NONE))
                WindowUIEffects = @default.WindowUIEffects;

            if (!OS.WXP && !SystemParametersInfo(SPI.SPI_GETCLIENTAREAANIMATION, 0, ref AnimateControlsInsideWindow, SPIF.SPIF_NONE))
                AnimateControlsInsideWindow = @default.AnimateControlsInsideWindow;

            if (!SystemParametersInfo(SPI.SPI_GETMENUANIMATION, 0, ref MenuAnimation, SPIF.SPIF_NONE))
                MenuAnimation = @default.MenuAnimation;

            if (!SystemParametersInfo(SPI.SPI_GETSELECTIONFADE, 0, ref MenuSelectionFade, SPIF.SPIF_NONE))
                MenuSelectionFade = @default.MenuSelectionFade;

            if (!SystemParametersInfo(SPI.SPI_GETMENUSHOWDELAY, 0, ref MenuShowDelay, SPIF.SPIF_NONE))
                MenuShowDelay = @default.MenuShowDelay;

            if (!SystemParametersInfo(SPI.SPI_GETCOMBOBOXANIMATION, 0, ref ComboBoxAnimation, SPIF.SPIF_NONE))
                ComboBoxAnimation = @default.ComboBoxAnimation;

            if (!SystemParametersInfo(SPI.SPI_GETLISTBOXSMOOTHSCROLLING, 0, ref ListBoxSmoothScrolling, SPIF.SPIF_NONE))
                ListBoxSmoothScrolling = @default.ListBoxSmoothScrolling;

            if (!SystemParametersInfo(SPI.SPI_GETTOOLTIPANIMATION, 0, ref TooltipAnimation, SPIF.SPIF_NONE))
                TooltipAnimation = @default.TooltipAnimation;

            if (!SystemParametersInfo(SPI.SPI_GETDRAGFULLWINDOWS, 0, ref ShowWinContentDrag, SPIF.SPIF_NONE))
                ShowWinContentDrag = @default.ShowWinContentDrag;

            if (!SystemParametersInfo(SPI.SPI_GETMENUUNDERLINES, 0, ref KeyboardUnderline, SPIF.SPIF_NONE))
                KeyboardUnderline = @default.KeyboardUnderline;

            if (!SystemParametersInfo(SPI.SPI_GETFOCUSBORDERWIDTH, 0, ref FocusRectWidth, SPIF.SPIF_NONE))
                FocusRectWidth = @default.FocusRectWidth;

            if (!SystemParametersInfo(SPI.SPI_GETFOCUSBORDERHEIGHT, 0, ref FocusRectHeight, SPIF.SPIF_NONE))
                FocusRectHeight = @default.FocusRectHeight;

            if (!SystemParametersInfo(SPI.SPI_GETCARETWIDTH, 0, ref Caret, SPIF.SPIF_NONE))
                Caret = @default.Caret;

            if (!SystemParametersInfo(SPI.SPI_GETACTIVEWINDOWTRACKING, 0, ref AWT_Enabled, SPIF.SPIF_NONE))
                AWT_Enabled = @default.AWT_Enabled;

            if (!SystemParametersInfo(SPI.SPI_GETACTIVEWNDTRKZORDER, 0, ref AWT_BringActivatedWindowToTop, SPIF.SPIF_NONE))
                AWT_BringActivatedWindowToTop = @default.AWT_BringActivatedWindowToTop;

            if (!SystemParametersInfo(SPI.SPI_GETACTIVEWNDTRKTIMEOUT, 0, ref AWT_Delay, SPIF.SPIF_NONE))
                AWT_Delay = @default.AWT_Delay;

            if (!SystemParametersInfo(SPI.SPI_GETSNAPTODEFBUTTON, 0, ref SnapCursorToDefButton, SPIF.SPIF_NONE))
                SnapCursorToDefButton = @default.SnapCursorToDefButton;

            ANIMATIONINFO anim = new();
            anim.cbSize = (uint)Marshal.SizeOf(anim);

            WindowAnimation = SystemParametersInfo(SPI.SPI_GETANIMATION, (int)anim.cbSize, ref anim, SPIF.SPIF_NONE) ? anim.IMinAnimate == 1 : @default.WindowAnimation;

            bool x = default;

            if (SystemParametersInfo(SPI.SPI_GETMENUFADE, 0, ref x, SPIF.SPIF_NONE))
            {
                MenuFade = x ? MenuAnimType.Fade : MenuAnimType.Scroll;
            }
            else
            {
                MenuFade = @default.MenuFade;
            }

            if (SystemParametersInfo(SPI.SPI_GETTOOLTIPFADE, 0, ref x, SPIF.SPIF_NONE))
            {
                TooltipFade = x ? MenuAnimType.Fade : MenuAnimType.Scroll;
            }
            else
            {
                TooltipFade = @default.TooltipFade;
            }

            IconsShadow = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewShadow", @default.IconsShadow));
            IconsDesktopTranslSel = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewAlphaSelect", @default.IconsDesktopTranslSel));
            NotificationDuration = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Accessibility", "MessageDuration", @default.NotificationDuration));
            ShowSecondsInSystemClock = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSecondsInSystemClock", @default.ShowSecondsInSystemClock));
            BalloonNotifications = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", @default.BalloonNotifications));
            PaintDesktopVersion = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "PaintDesktopVersion", @default.PaintDesktopVersion));
            ClassicVolMixer = !Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", !@default.ClassicVolMixer));
            EnableAeroPeek = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", @default.EnableAeroPeek));
            AlwaysHibernateThumbnails = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", @default.AlwaysHibernateThumbnails));

            bool temp = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", !@default.ShakeToMinimize));
            ShakeToMinimize = !temp;

            try
            {
                if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID").OpenSubKey(@"{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32") is not null)
                {
                    Win11ClassicContextMenu = true;
                }
                else
                {
                    Win11ClassicContextMenu = false;
                }
            }
            catch
            {
                Win11ClassicContextMenu = @default.Win11ClassicContextMenu;
            }
            finally
            {
                Microsoft.Win32.Registry.CurrentUser.Close();
            }

            try
            {
                if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID").OpenSubKey(@"{1eeb5b5a-06fb-4732-96b3-975c0194eb39}\InprocServer32") is not null)
                {
                    SysListView32 = true;
                }
                else
                {
                    SysListView32 = false;
                }
            }
            catch
            {
                SysListView32 = @default.SysListView32;
            }
            finally
            {
                Microsoft.Win32.Registry.CurrentUser.Close();
            }

            if (GetReg(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", null) is null)
            {
                Win11BootDots = !OS.W12 && !OS.W11;
            }

            else
            {
                switch (GetReg(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", (OS.W12 || OS.W11) ? 1 : 0))
                {
                    case 0:
                        Win11BootDots = true;
                        break;

                    case 1:
                        Win11BootDots = false;
                        break;

                    default:
                        Win11BootDots = false;
                        break;

                }
            }

            Win11ExplorerBar = (ExplorerBar)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "Win11ExplorerBar", @default.Win11ExplorerBar));

            try
            {
                if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID").OpenSubKey(@"{056440FD-8568-48e7-A632-72157243B55B}\InprocServer32") is not null)
                {
                    DisableNavBar = true;
                }
                else
                {
                    DisableNavBar = false;
                }
            }
            catch
            {
                DisableNavBar = @default.DisableNavBar;
            }
            finally
            {
                Microsoft.Win32.Registry.CurrentUser.Close();
            }

            AutoHideScrollBars = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Control Panel\Accessibility", "DynamicScrollbars", @default.AutoHideScrollBars));
            FullScreenStartMenu = Convert.ToBoolean(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "ForceStartSize", @default.FullScreenStartMenu ? 2 : 0)) == 2) ? true : false;
            ColorFilter_Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "Active", @default.ColorFilter_Enabled));
            ColorFilter = (ColorFilters)GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "FilterType", @default.ColorFilter);
        }

        /// <summary>
        /// Saves WinEffects data into registry
        /// </summary>
        /// <param name="TreeView">TreeView used as theme log</param>
        public async void Apply(TreeView TreeView = null, bool silent = false)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", string.Empty, Enabled);

            if (Enabled)
            {
                if (silent || !Program.Settings.ThemeApplyingBehavior.Show_WinEffects_Alert || Forms.WinEffectsAlert.ShowDialog() == DialogResult.OK)
                {
                    WinEffects WE = (WinEffects)Clone();

                    await Task.Run(() =>
                    {
                        ANIMATIONINFO anim = new();
                        anim.cbSize = (uint)Marshal.SizeOf(anim);
                        anim.IMinAnimate = WE.WindowAnimation ? 1 : 0;

                        SystemParametersInfo(TreeView, SPI.SPI_SETANIMATION, (int)anim.cbSize, ref anim, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETDROPSHADOW, 0, WE.WindowShadow, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETUIEFFECTS, 0, WE.WindowUIEffects, SPIF.SPIF_UPDATEINIFILE);

                        if (!OS.WXP) SystemParametersInfo(TreeView, SPI.SPI_SETCLIENTAREAANIMATION, 0, WE.AnimateControlsInsideWindow, SPIF.SPIF_UPDATEINIFILE);

                        SystemParametersInfo(TreeView, SPI.SPI_SETDRAGFULLWINDOWS, WE.ShowWinContentDrag, 0, SPIF.SPIF_UPDATEINIFILE);        // use uiParam not pvParam
                        SystemParametersInfo(TreeView, SPI.SPI_SETMENUANIMATION, 0, WE.MenuAnimation, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETMENUFADE, 0, WE.MenuFade == MenuAnimType.Fade, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETMENUSHOWDELAY, WE.MenuShowDelay, 0, SPIF.SPIF_UPDATEINIFILE);               // use uiParam not pvParam
                        SystemParametersInfo(TreeView, SPI.SPI_SETSELECTIONFADE, 0, WE.MenuSelectionFade, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETCOMBOBOXANIMATION, 0, WE.ComboBoxAnimation, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETLISTBOXSMOOTHSCROLLING, 0, WE.ListBoxSmoothScrolling, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETTOOLTIPANIMATION, 0, WE.TooltipAnimation, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETTOOLTIPFADE, 0, WE.TooltipFade == MenuAnimType.Fade, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETMENUUNDERLINES, 0, WE.KeyboardUnderline, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETFOCUSBORDERWIDTH, 0, WE.FocusRectWidth, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETFOCUSBORDERHEIGHT, 0, WE.FocusRectHeight, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETCARETWIDTH, 0, WE.Caret, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETACTIVEWINDOWTRACKING, 0, WE.AWT_Enabled, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETACTIVEWNDTRKZORDER, 0, WE.AWT_BringActivatedWindowToTop, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETACTIVEWNDTRKTIMEOUT, 0, WE.AWT_Delay, SPIF.SPIF_UPDATEINIFILE);
                        SystemParametersInfo(TreeView, SPI.SPI_SETSNAPTODEFBUTTON, WE.SnapCursorToDefButton, 0, SPIF.SPIF_UPDATEINIFILE);     // use uiParam not pvParam
                    });

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewShadow", IconsShadow ? 1 : 0);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewAlphaSelect", IconsDesktopTranslSel ? 1 : 0);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "MenuShowDelay", MenuShowDelay, Microsoft.Win32.RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Accessibility", "MessageDuration", NotificationDuration);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", (!ShakeToMinimize) ? 1 : 0);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSecondsInSystemClock", ShowSecondsInSystemClock ? 1 : 0);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "PaintDesktopVersion", PaintDesktopVersion ? 1 : 0);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", !ClassicVolMixer);

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Accessibility", "DynamicScrollbars", AutoHideScrollBars);

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "Active", ColorFilter_Enabled);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "FilterType", (int)ColorFilter);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows NT\CurrentVersion\Accessibility", "Configuration", ColorFilter_Enabled ? "colorfiltering" : string.Empty, Microsoft.Win32.RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", EnableAeroPeek ? 1 : 0);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", AlwaysHibernateThumbnails ? 1 : 0);

                    if (Program.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT)
                    {
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "PaintDesktopVersion", PaintDesktopVersion ? 1 : 0);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "CaretWidth", Caret);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "MenuShowDelay", MenuShowDelay);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "SnapToDefaultButton", SnapCursorToDefButton ? 1 : 0);
                    }

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "Win11ExplorerBar", Win11ExplorerBar);

                    try
                    {
                        if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\ExplorerPatcher") is not null)
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\ExplorerPatcher", "FileExplorerCommandUI", Win11ExplorerBar);
                        }
                    }
                    catch { } // Access to HKEY_CURRENT_USER\Software\ExplorerPatcher is denied, ignore it.
                    finally
                    {
                        Microsoft.Win32.Registry.CurrentUser.Close();
                    }

                    try
                    {
                        if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\StartIsBack") is not null)
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\StartIsBack", "FrameStyle", Win11ExplorerBar);
                        }
                    }
                    catch { } // Access to HKEY_CURRENT_USER\Software\ExplorerPatcher is denied, ignore it.
                    finally
                    {
                        Microsoft.Win32.Registry.CurrentUser.Close();
                    }

                    if (OS.W12 || OS.W11)
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", (!Win11BootDots) ? 1 : 0);

                    if (OS.W8x || OS.W10 || OS.W11 || OS.W12)
                    {
                        switch (Win11ExplorerBar)
                        {
                            case ExplorerBar.Bar:
                                if (System.IO.File.Exists($@"{SysPaths.System32}\UIRibbon.dll"))
                                {
                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, Program.Lang.Verbose_EnableExplorerBar, "file_rename");

                                    TakeOwn_File($@"{SysPaths.System32}\UIRibbon.dll");
                                    Move_File($@"{SysPaths.System32}\UIRibbon.dll", $@"{SysPaths.System32}\UIRibbon.dll_bak");

                                    // DelValue_AdministratorDeflector("HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID", "{926749fa-2615-4987-8845-c33e65f2b957}")
                                }

                                break;

                            default:
                                if (System.IO.File.Exists($@"{SysPaths.System32}\UIRibbon.dll_bak"))
                                {
                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, Program.Lang.Verbose_RestoreExplorerBar, "file_rename");

                                    TakeOwn_File($@"{SysPaths.System32}\UIRibbon.dll_bak");
                                    TakeOwn_File($@"{SysPaths.System32}\UIRibbon.dll");
                                    Move_File($@"{SysPaths.System32}\UIRibbon.dll_bak", $@"{SysPaths.System32}\UIRibbon.dll");
                                }

                                break;
                        }

                        if (OS.W11 || OS.W12)
                        {
                            if (Win11ExplorerBar != ExplorerBar.Default)
                            {
                                // Windows 11 22H2 and higher
                                try
                                {
                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{2aa9162e-c906-4dd9-ad0b-3d24a8eef5a0} > (default)", "reg_add");
                                    Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{2aa9162e-c906-4dd9-ad0b-3d24a8eef5a0}", true).SetValue(string.Empty, "CLSID_ItemsViewAdapter", Microsoft.Win32.RegistryValueKind.String);

                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{6480100b-5a83-4d1e-9f69-8ae5a88e9a33} > (default)", "reg_add");
                                    Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{6480100b-5a83-4d1e-9f69-8ae5a88e9a33}", true).SetValue(string.Empty, "File Explorer Xaml Island View Adapter", Microsoft.Win32.RegistryValueKind.String);

                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{2aa9162e-c906-4dd9-ad0b-3d24a8eef5a0} > InprocServer32", "reg_add");
                                    using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{2aa9162e-c906-4dd9-ad0b-3d24a8eef5a0}", true).CreateSubKey("InprocServer32", true))
                                    {
                                        key.SetValue(string.Empty, "C:\\Windows\\System32\\Windows.UI.FileExplorer.dll_", Microsoft.Win32.RegistryValueKind.String);
                                        key.SetValue("ThreadingModel", "Apartment", Microsoft.Win32.RegistryValueKind.String);
                                    }

                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{6480100b-5a83-4d1e-9f69-8ae5a88e9a33} > InprocServer32", "reg_add");
                                    using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{6480100b-5a83-4d1e-9f69-8ae5a88e9a33}", true).CreateSubKey("InprocServer32", true))
                                    {
                                        key.SetValue(string.Empty, $"{SysPaths.System32}\\Windows.UI.FileExplorer.dll_", Microsoft.Win32.RegistryValueKind.String);
                                        key.SetValue("ThreadingModel", "Apartment", Microsoft.Win32.RegistryValueKind.String);
                                    }
                                }
                                catch { } // Access to HKEY_CURRENT_USER\Software\Classes\CLSID\... is denied, ignore it.
                                finally
                                {
                                    Microsoft.Win32.Registry.CurrentUser.Close();
                                }

                                // Windows 11 lower than 22H2
                                try
                                {
                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_RegDelete, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked > {e2bf9676-5f8f-435c-97eb-11607a5bedf7}"), "reg_delete");
                                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked", true).DeleteValue("{e2bf9676-5f8f-435c-97eb-11607a5bedf7}", false);
                                }
                                catch { } // Access to HKEY_CURRENT_USER\Software\Classes\CLSID\... is denied, ignore it.
                                finally
                                {
                                    Microsoft.Win32.Registry.CurrentUser.Close();
                                }
                            }
                            else
                            {
                                try
                                {
                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{2aa9162e-c906-4dd9-ad0b-3d24a8eef5a0}"), "reg_delete");
                                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{2aa9162e-c906-4dd9-ad0b-3d24a8eef5a0}", false);

                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{6480100b-5a83-4d1e-9f69-8ae5a88e9a33}"), "reg_delete");
                                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{6480100b-5a83-4d1e-9f69-8ae5a88e9a33}", false);
                                }
                                catch { } // Access to HKEY_CURRENT_USER\Software\Classes\CLSID\... is denied, ignore it.
                                finally
                                {
                                    Microsoft.Win32.Registry.CurrentUser.Close();
                                }

                                EditReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Blocked", "{e2bf9676-5f8f-435c-97eb-11607a5bedf7}", string.Empty, Microsoft.Win32.RegistryValueKind.String);
                            }
                        }
                    }

                    // Try ... Catch is used as sometimes access to HKEY_CURRENT_USER\Software\Policies is denied
                    try
                    {
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", BalloonNotifications ? 1 : 0);
                    }
                    catch
                    {
                        EditReg_CMD(TreeView, @"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", BalloonNotifications ? 1 : 0);
                    }

                    // Try ... Catch is used as sometimes access to HKEY_CURRENT_USER\Software\Policies is denied
                    try
                    {
                        EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "ForceStartSize", FullScreenStartMenu ? 2 : 0);
                    }
                    catch
                    {
                        EditReg_CMD(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "ForceStartSize", FullScreenStartMenu ? 2 : 0);
                    }

                    if (OS.W12 || OS.W11)
                    {
                        try
                        {
                            if (Win11ClassicContextMenu)
                            {
                                if (TreeView is not null)
                                    Manager.AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2} > InprocServer32", "reg_add");
                                Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", true).CreateSubKey("InprocServer32", true).SetValue(string.Empty, string.Empty, Microsoft.Win32.RegistryValueKind.String);
                            }
                            else
                            {
                                if (TreeView is not null)
                                    Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}"), "reg_delete");
                                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", false);
                            }
                        }
                        catch { } // Access to HKEY_CURRENT_USER\Software\ExplorerPatcher is denied, ignore it.
                        finally
                        {
                            Microsoft.Win32.Registry.CurrentUser.Close();
                        }
                    }

                    if (!OS.WXP && !OS.WVista)
                    {
                        try
                        {
                            if (SysListView32)
                            {
                                if (TreeView is not null)
                                    Manager.AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39} > InprocServer32", "reg_add");
                                Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39}", true).CreateSubKey("InprocServer32", true).SetValue(string.Empty, string.Empty, Microsoft.Win32.RegistryValueKind.String);
                            }
                            else
                            {
                                if (TreeView is not null)
                                    Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39}"), "reg_delete");
                                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{1eeb5b5a-06fb-4732-96b3-975c0194eb39}", false);
                            }
                        }
                        catch { } // Access to HKEY_CURRENT_USER\Software\ExplorerPatcher is denied, ignore it.
                        finally
                        {
                            Microsoft.Win32.Registry.CurrentUser.Close();
                        }
                    }

                    try
                    {
                        if (DisableNavBar)
                        {
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}, InprocServer32", "reg_add");
                            Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}", true).CreateSubKey("InprocServer32", true).SetValue(string.Empty, string.Empty, Microsoft.Win32.RegistryValueKind.String);
                        }
                        else
                        {
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}"), "reg_delete");
                            Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{056440FD-8568-48e7-A632-72157243B55B}", false);
                        }
                    }
                    catch { } // Access to HKEY_CURRENT_USER\Software\ExplorerPatcher is denied, ignore it.
                    finally
                    {
                        Microsoft.Win32.Registry.CurrentUser.Close();
                    }
                }
            }
        }

        /// <summary>Operator to check if two WinEffects structures are equal</summary>
        public static bool operator ==(WinEffects First, WinEffects Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two WinEffects structures are not equal</summary>
        public static bool operator !=(WinEffects First, WinEffects Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones WinEffects structure</summary>
        public readonly object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two WinEffects structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of WinEffects structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
