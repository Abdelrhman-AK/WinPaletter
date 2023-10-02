using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static WinPaletter.Metrics;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme.Structures
{
    public struct WinEffects : ICloneable
    {
        public bool Enabled;

        public bool WindowAnimation;
        public bool WindowShadow;
        public bool WindowUIEffects;
        public bool ShowWinContentDrag;
        public bool AnimateControlsInsideWindow;

        public bool MenuAnimation;
        public MenuAnimType MenuFade;
        public bool MenuSelectionFade;
        public uint MenuShowDelay;            // Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer

        public bool ComboBoxAnimation;
        public bool ListBoxSmoothScrolling;

        public bool TooltipAnimation;
        public MenuAnimType TooltipFade;

        public bool IconsShadow;
        public bool IconsDesktopTranslSel;

        public bool KeyboardUnderline;
        public uint FocusRectWidth;           // Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer
        public uint FocusRectHeight;          // Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer
        public uint Caret;
        public int NotificationDuration;
        public bool ShakeToMinimize;
        public bool AWT_Enabled;
        public bool AWT_BringActivatedWindowToTop;
        public int AWT_Delay;
        public bool SnapCursorToDefButton;

        public bool Win11ClassicContextMenu;
        public bool SysListView32;
        public bool ShowSecondsInSystemClock;
        public bool BalloonNotifications;
        public bool PaintDesktopVersion;

        public bool Win11BootDots;
        public ExplorerBar Win11ExplorerBar;
        public bool DisableNavBar;

        public bool AutoHideScrollBars;
        public bool FullScreenStartMenu;
        public bool ColorFilter_Enabled;
        public ColorFilters ColorFilter;

        public bool ClassicVolMixer;

        public enum ExplorerBar
        {
            Default,
            Ribbon,
            Bar
        }

        public enum ColorFilters
        {
            Grayscale,
            Inverted,
            GrayscaleInverted,
            RedGreen_deuteranopia,
            RedGreen_protanopia,
            BlueYellow
        }

        public enum MenuAnimType
        {
            Fade,
            Scroll
        }

        public void Load(WinEffects _DefEffects)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "", true));

            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETDROPSHADOW, 0, ref WindowShadow, (int)SPIF.None) == 0)
                WindowShadow = _DefEffects.WindowShadow;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETUIEFFECTS, 0, ref WindowUIEffects, (int)SPIF.None) == 0)
                WindowUIEffects = _DefEffects.WindowUIEffects;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETCLIENTAREAANIMATION, 0, ref AnimateControlsInsideWindow, (int)SPIF.None) == 0)
                AnimateControlsInsideWindow = _DefEffects.AnimateControlsInsideWindow;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETMENUANIMATION, 0, ref MenuAnimation, (int)SPIF.None) == 0)
                MenuAnimation = _DefEffects.MenuAnimation;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETSELECTIONFADE, 0, ref MenuSelectionFade, (int)SPIF.None) == 0)
                MenuSelectionFade = _DefEffects.MenuSelectionFade;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETMENUSHOWDELAY, 0, ref MenuShowDelay, (int)SPIF.None) == 0)
                MenuShowDelay = _DefEffects.MenuShowDelay;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETCOMBOBOXANIMATION, 0, ref ComboBoxAnimation, (int)SPIF.None) == 0)
                ComboBoxAnimation = _DefEffects.ComboBoxAnimation;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETLISTBOXSMOOTHSCROLLING, 0, ref ListBoxSmoothScrolling, (int)SPIF.None) == 0)
                ListBoxSmoothScrolling = _DefEffects.ListBoxSmoothScrolling;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETTOOLTIPANIMATION, 0, ref TooltipAnimation, (int)SPIF.None) == 0)
                TooltipAnimation = _DefEffects.TooltipAnimation;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETDRAGFULLWINDOWS, 0, ref ShowWinContentDrag, (int)SPIF.None) == 0)
                ShowWinContentDrag = _DefEffects.ShowWinContentDrag;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETMENUUNDERLINES, 0, ref KeyboardUnderline, (int)SPIF.None) == 0)
                KeyboardUnderline = _DefEffects.KeyboardUnderline;
            if (Fixer.SystemParametersInfo((int)SPI.FocusRect.GETFOCUSBORDERWIDTH, 0, ref FocusRectWidth, (int)SPIF.None) == 0)
                FocusRectWidth = _DefEffects.FocusRectWidth;
            if (Fixer.SystemParametersInfo((int)SPI.FocusRect.GETFOCUSBORDERHEIGHT, 0, ref FocusRectHeight, (int)SPIF.None) == 0)
                FocusRectHeight = _DefEffects.FocusRectHeight;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETCARETWIDTH, 0, ref Caret, (int)SPIF.None) == 0)
                Caret = _DefEffects.Caret;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETACTIVEWINDOWTRACKING, 0, ref AWT_Enabled, (int)SPIF.None) == 0)
                AWT_Enabled = _DefEffects.AWT_Enabled;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETACTIVEWNDTRKZORDER, 0, ref AWT_BringActivatedWindowToTop, (int)SPIF.None) == 0)
                AWT_BringActivatedWindowToTop = _DefEffects.AWT_BringActivatedWindowToTop;
            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETACTIVEWNDTRKTIMEOUT, 0, ref AWT_Delay, (int)SPIF.None) == 0)
                AWT_Delay = _DefEffects.AWT_Delay;
            if (Fixer.SystemParametersInfo((int)SPI.Cursors.GETSNAPTODEFBUTTON, 0, ref SnapCursorToDefButton, (int)SPIF.None) == 0)
                SnapCursorToDefButton = _DefEffects.SnapCursorToDefButton;

            ANIMATIONINFO anim = new ANIMATIONINFO();
            anim.cbSize = (uint)Marshal.SizeOf(anim);

            if (SystemParametersInfo((int)SPI.Effects.GETANIMATION, (int)anim.cbSize, ref anim, SPIF.None) == 1)
            {
                WindowAnimation = anim.IMinAnimate.ToBoolean();
            }
            else
            {
                WindowAnimation = _DefEffects.WindowAnimation;
            }

            var x = default(bool);

            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETMENUFADE, 0, ref x, (int)SPIF.None) == 1)
            {
                MenuFade = x ? MenuAnimType.Fade : MenuAnimType.Scroll;
            }
            else
            {
                MenuFade = _DefEffects.MenuFade;
            }

            if (Fixer.SystemParametersInfo((int)SPI.Effects.GETTOOLTIPFADE, 0, ref x, (int)SPIF.None) == 1)
            {
                TooltipFade = x ? MenuAnimType.Fade : MenuAnimType.Scroll;
            }
            else
            {
                TooltipFade = _DefEffects.TooltipFade;
            }

            IconsShadow = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewShadow", _DefEffects.IconsShadow));
            IconsDesktopTranslSel = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewAlphaSelect", _DefEffects.IconsDesktopTranslSel));
            NotificationDuration = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Accessibility", "MessageDuration", _DefEffects.NotificationDuration));
            ShowSecondsInSystemClock = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSecondsInSystemClock", _DefEffects.ShowSecondsInSystemClock));
            BalloonNotifications = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", _DefEffects.BalloonNotifications));
            PaintDesktopVersion = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "PaintDesktopVersion", _DefEffects.PaintDesktopVersion));
            ClassicVolMixer = !Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", !_DefEffects.ClassicVolMixer));

            bool temp = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", !_DefEffects.ShakeToMinimize));
            ShakeToMinimize = !temp;

            try
            {
                if (My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID").OpenSubKey(@"{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32") is not null)
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
                Win11ClassicContextMenu = _DefEffects.Win11ClassicContextMenu;
            }
            finally
            {
                My.MyProject.Computer.Registry.CurrentUser.Close();
            }

            try
            {
                if (My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID").OpenSubKey(@"{1eeb5b5a-06fb-4732-96b3-975c0194eb39}\InprocServer32") is not null)
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
                SysListView32 = _DefEffects.SysListView32;
            }
            finally
            {
                My.MyProject.Computer.Registry.CurrentUser.Close();
            }

            if (GetReg(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", null) is null)
            {
                Win11BootDots = !My.Env.W11;
            }

            else
            {
                switch (GetReg(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", My.Env.W11 ? 1 : 0))
                {
                    case 0:
                        {
                            Win11BootDots = true;
                            break;
                        }

                    case 1:
                        {
                            Win11BootDots = false;
                            break;
                        }

                    default:
                        {
                            Win11BootDots = false;
                            break;
                        }

                }
            }

            Win11ExplorerBar = (ExplorerBar)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "Win11ExplorerBar", _DefEffects.Win11ExplorerBar));

            try
            {
                if (My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID").OpenSubKey(@"{056440FD-8568-48e7-A632-72157243B55B}\InprocServer32") is not null)
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
                DisableNavBar = _DefEffects.DisableNavBar;
            }
            finally
            {
                My.MyProject.Computer.Registry.CurrentUser.Close();
            }

            AutoHideScrollBars = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Control Panel\Accessibility", "DynamicScrollbars", _DefEffects.AutoHideScrollBars));
            FullScreenStartMenu = Convert.ToBoolean(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "ForceStartSize", _DefEffects.FullScreenStartMenu ? 2 : 0)) == 2) ? true : false;
            ColorFilter_Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "Active", _DefEffects.ColorFilter_Enabled));
            ColorFilter = (ColorFilters)GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "FilterType", _DefEffects.ColorFilter);
        }

        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "", Enabled);

            if (Enabled)
            {
                ANIMATIONINFO anim = new ANIMATIONINFO();
                anim.cbSize = (uint)Marshal.SizeOf(anim);
                anim.IMinAnimate = WindowAnimation.ToInteger();

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETANIMATION.ToString(), anim.cbSize, anim.ToString(), SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETANIMATION, (int)anim.cbSize, ref anim, SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETDROPSHADOW.ToString(), 0, WindowShadow, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETDROPSHADOW, 0, WindowShadow, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETUIEFFECTS.ToString(), 0, WindowUIEffects, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETUIEFFECTS, 0, WindowUIEffects, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETCLIENTAREAANIMATION.ToString(), 0, AnimateControlsInsideWindow, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETCLIENTAREAANIMATION, 0, AnimateControlsInsideWindow, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETDRAGFULLWINDOWS.ToString(), 0, ShowWinContentDrag, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETDRAGFULLWINDOWS, ShowWinContentDrag, 0, (int)SPIF.UpdateINIFile);        // use uiParam not pvParam

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETMENUANIMATION.ToString(), 0, MenuAnimation, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETMENUANIMATION, 0, MenuAnimation, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETMENUFADE.ToString(), 0, MenuFade == MenuAnimType.Fade, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETMENUFADE, 0, MenuFade == MenuAnimType.Fade, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETMENUSHOWDELAY.ToString(), MenuShowDelay, 0, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETMENUSHOWDELAY, MenuShowDelay, 0, (int)SPIF.UpdateINIFile);               // use uiParam not pvParam

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETSELECTIONFADE.ToString(), 0, MenuSelectionFade, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETSELECTIONFADE, 0, MenuSelectionFade, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETCOMBOBOXANIMATION.ToString(), 0, ComboBoxAnimation, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETCOMBOBOXANIMATION, 0, ComboBoxAnimation, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETLISTBOXSMOOTHSCROLLING.ToString(), 0, ListBoxSmoothScrolling, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETLISTBOXSMOOTHSCROLLING, 0, ListBoxSmoothScrolling, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETTOOLTIPANIMATION.ToString(), 0, TooltipAnimation, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETTOOLTIPANIMATION, 0, TooltipAnimation, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETTOOLTIPFADE.ToString(), 0, TooltipFade == MenuAnimType.Fade, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETTOOLTIPFADE, 0, TooltipFade == MenuAnimType.Fade, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETMENUUNDERLINES.ToString(), 0, KeyboardUnderline, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETMENUUNDERLINES, 0, KeyboardUnderline, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.FocusRect.SETFOCUSBORDERWIDTH.ToString(), 0, FocusRectWidth, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.FocusRect.SETFOCUSBORDERWIDTH, 0, FocusRectWidth, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.FocusRect.SETFOCUSBORDERHEIGHT.ToString(), 0, FocusRectHeight, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.FocusRect.SETFOCUSBORDERHEIGHT, 0, FocusRectHeight, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETCARETWIDTH.ToString(), 0, Caret, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETCARETWIDTH, 0, Caret, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETACTIVEWINDOWTRACKING.ToString(), 0, AWT_Enabled, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETACTIVEWINDOWTRACKING, 0, AWT_Enabled, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETACTIVEWNDTRKZORDER.ToString(), 0, AWT_BringActivatedWindowToTop, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETACTIVEWNDTRKZORDER, 0, AWT_BringActivatedWindowToTop, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETACTIVEWNDTRKTIMEOUT.ToString(), 0, AWT_Delay, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Effects.SETACTIVEWNDTRKTIMEOUT, 0, AWT_Delay, (int)SPIF.UpdateINIFile);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETSNAPTODEFBUTTON.ToString(), 0, SnapCursorToDefButton, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Cursors.SETSNAPTODEFBUTTON, SnapCursorToDefButton, 0, (int)SPIF.UpdateINIFile);     // use uiParam not pvParam

                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewShadow", IconsShadow.ToInteger());
                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewAlphaSelect", IconsDesktopTranslSel.ToInteger());
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "MenuShowDelay", MenuShowDelay, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Accessibility", "MessageDuration", NotificationDuration);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", (!ShakeToMinimize).ToInteger());
                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSecondsInSystemClock", ShowSecondsInSystemClock.ToInteger());
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "PaintDesktopVersion", PaintDesktopVersion.ToInteger());
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", !ClassicVolMixer);

                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Accessibility", "DynamicScrollbars", AutoHideScrollBars);

                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "Active", ColorFilter_Enabled);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "FilterType", (int)ColorFilter);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows NT\CurrentVersion\Accessibility", "Configuration", ColorFilter_Enabled ? "colorfiltering" : "", RegistryValueKind.String);

                if (My.Env.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT)
                {
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "PaintDesktopVersion", PaintDesktopVersion.ToInteger());
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "CaretWidth", Caret);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "MenuShowDelay", MenuShowDelay);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "SnapToDefaultButton", SnapCursorToDefButton.ToInteger());
                }

                try
                {
                    if (My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\ExplorerPatcher") is not null)
                    {
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\ExplorerPatcher", "FileExplorerCommandUI", Win11ExplorerBar);
                    }
                }
                catch
                {
                    // Do nothing
                    My.MyProject.Computer.Registry.CurrentUser.Close();
                }
                finally
                {
                    My.MyProject.Computer.Registry.CurrentUser.Close();
                }

                try
                {
                    if (My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\StartIsBack") is not null)
                    {
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\StartIsBack", "FrameStyle", Win11ExplorerBar);
                    }
                }
                catch
                {
                    // Do nothing
                    My.MyProject.Computer.Registry.CurrentUser.Close();
                }
                finally
                {
                    My.MyProject.Computer.Registry.CurrentUser.Close();
                }

                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "Win11ExplorerBar", Win11ExplorerBar);

                if (My.Env.W11)
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", (!Win11BootDots).ToInteger());

                if (My.Env.W8 | My.Env.W81 || My.Env.W10)
                {
                    switch (Win11ExplorerBar)
                    {
                        case ExplorerBar.Bar:
                            {
                                if (System.IO.File.Exists(My.Env.PATH_System32 + @"\UIRibbon.dll"))
                                {
                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, My.Env.Lang.Verbose_EnableExplorerBar, "file_rename");

                                    Takeown_File(My.Env.PATH_System32 + @"\UIRibbon.dll");
                                    Move_File(My.Env.PATH_System32 + @"\UIRibbon.dll", My.Env.PATH_System32 + @"\UIRibbon.dll_bak");

                                    // DelReg_AdministratorDeflector("HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID", "{926749fa-2615-4987-8845-c33e65f2b957}")

                                }

                                break;
                            }

                        default:
                            {
                                if (System.IO.File.Exists(My.Env.PATH_System32 + @"\UIRibbon.dll_bak"))
                                {
                                    if (TreeView is not null)
                                        Manager.AddNode(TreeView, My.Env.Lang.Verbose_RestoreExplorerBar, "file_rename");

                                    Takeown_File(My.Env.PATH_System32 + @"\UIRibbon.dll_bak");
                                    Takeown_File(My.Env.PATH_System32 + @"\UIRibbon.dll");
                                    Move_File(My.Env.PATH_System32 + @"\UIRibbon.dll_bak", My.Env.PATH_System32 + @"\UIRibbon.dll");
                                }

                                break;
                            }

                            // TakeOwn_Reg(Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\CLSID"), "{926749fa-2615-4987-8845-c33e65f2b957}")
                            // EditReg_CMD([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\{926749fa-2615-4987-8845-c33e65f2b957}", "", "%SystemRoot%\system32\UIRibbon.dll", RegistryValueKind.ExpandString)
                            // EditReg_CMD([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\{926749fa-2615-4987-8845-c33e65f2b957}", "ThreadingModel", "Apartment", RegistryValueKind.String)

                    }
                }

                // Try ... Catch is used as sometimes access to HKEY_CURRENT_USER\Software\Policies is denied
                try
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", BalloonNotifications.ToInteger());
                }
                catch
                {
                    EditReg_CMD(TreeView, @"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", BalloonNotifications.ToInteger());
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

                if (My.Env.W11)
                {
                    try
                    {
                        if (Win11ClassicContextMenu)
                        {
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2} > InprocServer32", "reg_add");
                            My.MyProject.Computer.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", true).CreateSubKey("InprocServer32", true).SetValue("", "", RegistryValueKind.String);
                        }
                        else
                        {
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}"), "reg_delete");
                            My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", false);
                        }
                    }
                    catch
                    {
                        // Do nothing
                        My.MyProject.Computer.Registry.CurrentUser.Close();
                    }
                    finally
                    {
                        My.MyProject.Computer.Registry.CurrentUser.Close();
                    }
                }

                if (!My.Env.WXP && !My.Env.WVista)
                {
                    try
                    {
                        if (SysListView32)
                        {
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39} > InprocServer32", "reg_add");
                            My.MyProject.Computer.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39}", true).CreateSubKey("InprocServer32", true).SetValue("", "", RegistryValueKind.String);
                        }
                        else
                        {
                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39}"), "reg_delete");
                            My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{1eeb5b5a-06fb-4732-96b3-975c0194eb39}", false);
                        }
                    }
                    catch
                    {
                        // Do nothing
                        My.MyProject.Computer.Registry.CurrentUser.Close();
                    }
                    finally
                    {
                        My.MyProject.Computer.Registry.CurrentUser.Close();
                    }
                }

                try
                {
                    if (DisableNavBar)
                    {
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}, InprocServer32", "reg_add");
                        My.MyProject.Computer.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}", true).CreateSubKey("InprocServer32", true).SetValue("", "", RegistryValueKind.String);
                    }
                    else
                    {
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}"), "reg_delete");
                        My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{056440FD-8568-48e7-A632-72157243B55B}", false);
                    }
                }
                catch
                {
                    // Do nothing
                    My.MyProject.Computer.Registry.CurrentUser.Close();
                }
                finally
                {
                    My.MyProject.Computer.Registry.CurrentUser.Close();
                }

            }
        }

        public static bool operator ==(WinEffects First, WinEffects Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(WinEffects First, WinEffects Second)
        {
            return !First.Equals(Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
