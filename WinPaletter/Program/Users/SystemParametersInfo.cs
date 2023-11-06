using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using static WinPaletter.Metrics;

namespace WinPaletter.NativeMethods
{
    public partial class User32
    {
        private partial class PrivateFunctions
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int SystemParametersInfo(int uAction, int uParam, ref NONCLIENTMETRICS lpvParam, SPIF fuWinIni);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int SystemParametersInfo(int uAction, int uParam, ref ICONMETRICS lpvParam, SPIF fuWinIni);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int SystemParametersInfo(int uAction, int uParam, ref ANIMATIONINFO lpvParam, SPIF fuWinIni);

            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, int uParam, ref bool lpvParam, int fuWinIni);

            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, int uParam, ref int lpvParam, int fuWinIni);

            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, int uParam, ref uint lpvParam, int fuWinIni);

            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, int uParam, int lpvParam, int fuWinIni);

            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, int uParam, uint lpvParam, int fuWinIni);

            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, uint uParam, int lpvParam, int fuWinIni);

            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, int uParam, bool lpvParam, int fuWinIni);

            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, bool uParam, int lpvParam, int fuWinIni);
        }

        public static bool SystemParametersInfo(int uAction, int uParam, ref NONCLIENTMETRICS lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, int uParam, ref NONCLIENTMETRICS lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, int uParam, ref ICONMETRICS lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, int uParam, ref ICONMETRICS lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, int uParam, ref ANIMATIONINFO lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, int uParam, ref ANIMATIONINFO lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, int uParam, ref bool lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, (int)fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                advapi.RevertToSelf();

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, int uParam, ref bool lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, int uParam, ref int lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, (int)fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, int uParam, ref int lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, int uParam, ref uint lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, (int)fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, int uParam, ref uint lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, int uParam, int lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, (int)fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, int uParam, int lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, int uParam, uint lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, (int)fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, int uParam, uint lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, uint uParam, int lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, (int)fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, uint uParam, int lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, int uParam, string lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, (int)fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, int uParam, string lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, int uParam, bool lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, (int)fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, int uParam, bool lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, TreeView);
        }


        public static bool SystemParametersInfo(int uAction, bool uParam, int lpvParam, SPIF fuWinIni, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, (int)fuWinIni) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "user32.dll", "SystemParameterInfo", uAction, uParam, lpvParam, fuWinIni, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SystemParametersInfo(TreeView TreeView, int uAction, bool uParam, int lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, TreeView);
        }


        /// <summary>
        /// SPI: System-wide parameter - Used in SystemParametersInfo function
        /// </summary>
        public class SPI
        {

            public enum Icons : int
            {
                /// <summary>
                /// <b>Sets or retrieves the width, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large icon view.</b>
                /// <br></br>
                /// <br></br> • To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CXICON.
                /// <br></br> • To retrieve this value, pvParam must point to an integer that receives the current value.
                /// </summary>
                ICONHORIZONTALSPACING = 0xD,

                /// <summary>
                /// <b>Sets or retrieves the height, in pixels, of an icon cell.</b>
                /// <br></br>
                /// <br></br> • To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CYICON.
                /// <br></br> • To retrieve this value, pvParam must point to an integer that receives the current value.
                /// </summary>
                ICONVERTICALSPACING = 0x18,

                /// <summary>
                /// <b>Retrieves the logical font information for the current icon-title font.</b>
                /// <br></br>
                /// <br></br> • The uiParam parameter specifies the size of a LOGFONT structure.
                /// <br></br> • The pvParam parameter must point to the LOGFONT structure to fill in.
                /// </summary>
                GETICONTITLELOGFONT = 0x1F,

                /// <summary>
                /// <b>Sets the font that is used for icon titles.</b>
                /// <br></br>
                /// <br></br> • The uiParam parameter specifies the size of a LOGFONT structure.
                /// <br></br> • The pvParam parameter must point to a LOGFONT structure.
                /// </summary>
                SETICONTITLELOGFONT = 0x22,

                /// <summary>
                /// <b>Retrieves the metrics associated with icons.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to an ICONMETRICS structure that receives the information.
                /// <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
                /// </summary>
                GETICONMETRICS = 0x2D,

                /// <summary>
                /// <b>Sets the metrics associated with icons.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to an ICONMETRICS structure that contains the new parameters. 
                /// <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
                /// </summary>
                SETICONMETRICS = 0x2E,

                /// <summary>
                /// <b>Reloads the system icons.</b>
                /// <br></br>
                /// <br></br> • Set the uiParam parameter to zero.
                /// <br></br> • Set the pvParam parameter to null.
                /// </summary>
                SETICONS = 0x58

            }

            public enum Desktop : int
            {
                /// <summary>
                /// <b>Sets the desktop wallpaper.</b>
                /// <br></br>
                /// <br></br> • The value of the pvParam parameter determines the new wallpaper. 
                /// <br></br> • To specify a wallpaper bitmap, set pvParam to point to a null-terminated string containing the name of a bitmap file. Setting pvParam to "" removes the wallpaper.
                /// <br></br> • Setting pvParam to SETWALLPAPER_DEFAULT or null reverts to the default wallpaper.
                /// </summary>
                SETDESKWALLPAPER = 0x14,

                /// <summary>
                /// <b>Sets the current desktop pattern by causing Windows to read the Pattern= setting from the WIN.INI file.</b>
                /// </summary>
                SETDESKPATTERN = 0x15
            }

            public enum Metrics : int
            {
                /// <summary>
                /// <b>Retrieves the metrics associated with the nonclient area of nonminimized windows.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a NONCLIENTMETRICS structure that receives the information. Set the cbSize member of this structure.
                /// <br></br> • The uiParam parameter to sizeof(NONCLIENTMETRICS).
                /// </summary>
                GETNONCLIENTMETRICS = 0x29,

                /// <summary>
                /// <b>Sets the metrics associated with the nonclient area of nonminimized windows.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a NONCLIENTMETRICS structure that contains the new parameters. 
                /// <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(NONCLIENTMETRICS). Also, the lfHeight member of the LOGFONT structure must be a negative value.
                /// </summary>
                SETNONCLIENTMETRICS = 0x2A
            }

            public enum Cursors : int
            {
                /// <summary>
                /// <b>Reloads the system cursors.</b>
                /// <br></br>
                /// <br></br> • Set the uiParam parameter to zero.
                /// <br></br> • Set the pvParam parameter to null.
                /// </summary>
                SETCURSORS = 0x57,

                /// <summary>
                /// <b>Determines whether the cursor has a shadow around it.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives True if the shadow is enabled, False if it is disabled.
                /// <br></br> • This effect appears only if the system has a color depth of more than 256 colors.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value Is not supported.</i>
                /// </summary>
                GETCURSORSHADOW = 0x101A,

                /// <summary>
                /// <b>Enables or disables a shadow around the cursor.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter is a BOOL variable. Set pvParam to 1 to enable the shadow or 0 to disable the shadow.
                /// <br></br> • This effect appears only if the system has a color depth of more than 256 colors.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETCURSORSHADOW = 0x101B,

                /// <summary>
                /// <b>Determines whether the Mouse Trails feature is enabled. This feature improves the visibility of mouse cursor movements by briefly showing a trail of cursors and quickly erasing them.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to an integer variable that receives a value.
                /// <br></br> • If the value is zero or 1, the feature is disabled. If the value Is greater than 1, the feature Is enabled And the value indicates the number of cursors drawn in the trail.
                /// <br></br> • The uiParam parameter is not used.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT:  This value is not supported.</i>
                /// </summary>
                GETMOUSETRAILS = 0x5E,

                /// <summary>
                /// <b>Enables or disables the Mouse Trails feature, which improves the visibility of mouse cursor movements by briefly showing a trail of cursors and quickly erasing them.</b>
                /// <br></br>
                /// <br></br> • To disable the feature, set the uiParam parameter to zero or 1.
                /// <br></br> • To enable the feature, set uiParam to a value greater than 1 to indicate the number of cursors drawn in the trail.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT:  This value is not supported.</i>
                /// </summary>
                SETMOUSETRAILS = 0x5D,

                /// <summary>
                /// <b>Retrieves the state of the Mouse Sonar feature.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled or FALSE otherwise.
                /// <br></br> • For more information, see About Mouse Input on MSDN.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows 98/95:  This value is not supported.</i>
                /// </summary>
                GETMOUSESONAR = 0x101C,

                /// <summary>
                /// <b>Turns the Sonar accessibility feature on or off. This feature briefly shows several concentric circles around the mouse pointer when the user presses And releases the CTRL key.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter specifies TRUE for on and FALSE for off. The default is off.
                /// <br></br> • For more information, see About Mouse Input on MSDN.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows 98/95:  This value is not supported.</i>
                /// </summary>
                SETMOUSESONAR = 0x101D,

                /// <summary>
                /// <b>Determines whether the snap-to-default-button feature is enabled. If enabled, the mouse cursor automatically moves to the default button, such as OK Or Apply, of a dialog box.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if the feature Is on, Or FALSE if it Is off.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  Not supported.</i>
                /// </summary>
                GETSNAPTODEFBUTTON = 0x5F,

                /// <summary>
                /// <b>Enables or disables the snap-to-default-button feature. If enabled, the mouse cursor automatically moves to the default button, such as OK Or Apply, of a dialog box.</b>
                /// <br></br>
                /// <br></br> • Set the uiParam parameter to TRUE to enable the feature, or FALSE to disable it.
                /// <br></br> • Applications should use the ShowWindow function when displaying a dialog box so the dialog manager can position the mouse cursor.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  Not supported.</i>
                /// </summary>
                SETSNAPTODEFBUTTON = 0x60
            }

            public enum Titlebars : int
            {
                /// <summary>
                /// <b>Determines whether the gradient effect for window title bars is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
                /// <br></br> • For more information about the gradient effect, see the GetSysColor function.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETGRADIENTCAPTIONS = 0x1008,

                /// <summary>
                /// <b>Enables or disables the gradient effect for window title bars.</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter to 1 to enable it, or 0 to disable it.
                /// <br></br> • The gradient effect is possible only if the system has a color depth of more than 256 colors.
                /// <br></br> • For more information about the gradient effect, see the GetSysColor function.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETGRADIENTCAPTIONS = 0x1009
            }

            public enum Effects : int
            {
                /// <summary>
                /// <b>Retrieves the animation effects associated with user actions.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to an ANIMATIONINFO structure that receives the information. 
                /// <br></br> • Set the cbSize member of this structure.
                /// <br></br> • Set the uiParam parameter to sizeof(ANIMATIONINFO).
                /// </summary>
                GETANIMATION = 0x48,

                /// <summary>
                /// <b>Sets the animation effects associated with user actions.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to an ANIMATIONINFO structure that contains the new parameters.
                /// <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(ANIMATIONINFO).
                /// </summary>
                SETANIMATION = 0x49,

                /// <summary>
                /// <b>Retrieves the animation of controls and elements inside window.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is disabled.
                /// <br></br>
                /// </summary>
                GETCLIENTAREAANIMATION = 0x1042,

                /// <summary>
                /// <b>Sets the animation of controls and elements inside window.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is disabled.
                /// <br></br>
                /// </summary>
                SETCLIENTAREAANIMATION = 0x1043,

                /// <summary>
                /// <b>Determines whether the menu animation feature is enabled. This master switch must be on to enable menu animation effects.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is disabled.
                /// <br></br> • If animation is enabled, GETMENUFADE indicates whether menus use fade or slide animation.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETMENUANIMATION = 0x1002,

                /// <summary>
                /// <b>Enables or disables menu animation. This master switch must be on for any menu animation to occur.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter is a BOOL variable; set pvParam to 1 to enable animation and 0 to disable animation.
                /// <br></br> • If animation is enabled, GETMENUFADE indicates whether menus use fade or slide animation.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETMENUANIMATION = 0x1003,

                /// <summary>
                /// <b>Determines whether the slide-open effect for combo boxes is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETCOMBOBOXANIMATION = 0x1004,

                /// <summary>
                /// <b>Enables or disables the slide-open effect for combo boxes.</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter to 1 to enable the gradient effect, or 0 to disable it.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETCOMBOBOXANIMATION = 0x1005,

                /// <summary>
                /// <b>Determines whether the smooth-scrolling effect for list boxes is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETLISTBOXSMOOTHSCROLLING = 0x1006,

                /// <summary>
                /// <b>Enables or disables the smooth-scrolling effect for list boxes.</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter to 1 to enable the smooth-scrolling effect, or 0 to disable it.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETLISTBOXSMOOTHSCROLLING = 0x1007,

                /// <summary>
                /// <b>Determines whether menu fade animation is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE when fade animation is enabled and FALSE when it is disabled.
                /// <br></br> • If fade animation is disabled, menus use slide animation. This flag is ignored unless menu animation is enabled, which you can do using the SETMENUANIMATION flag.
                /// <br></br> • For more information, see AnimateWindow.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETMENUFADE = 0x1012,

                /// <summary>
                /// <b>Enables or disables menu fade animation.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to 1 to enable the menu fade effect or 0 to disable it (i.e. use slide animation).
                /// <br></br> • If fade animation is disabled, menus use slide animation. he The menu fade effect is possible only if the system has a color depth of more than 256 colors.
                /// <br></br> • This flag is ignored unless SPI_MENUANIMATION is also set.
                /// <br></br> • For more information, see AnimateWindow.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETMENUFADE = 0x1013,

                /// <summary>
                /// <b>Retrieves the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is over a submenu item.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a DWORD variable that receives the time of the delay.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  Not supported.</i>
                /// </summary>
                GETMENUSHOWDELAY = 0x6A,

                /// <summary>
                /// <b>Sets the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is over a submenu item.</b>
                /// <br></br>
                /// <br></br> • The uiParam parameter must point to a DWORD variable that sets the time of the delay.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  Not supported.</i>
                /// </summary>
                SETMENUSHOWDELAY = 0x6B,

                /// <summary>
                /// <b>Determines whether the selection fade effect is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled or FALSE if disabled.
                /// <br></br> • The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out after the menu is dismissed.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETSELECTIONFADE = 0x1014,

                /// <summary>
                /// <b>The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out after the menu is dismissed. The selection fade effect is possible only if the system has a color depth of more than 256 colors.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to 1 to enable the selection fade effect or 0 to disable it.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETSELECTIONFADE = 0x1015,

                /// <summary>
                /// <b>Determines whether ToolTip animation is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled or FALSE if disabled.
                /// <br></br> • If ToolTip animation is enabled, GETTOOLTIPFADE indicates whether ToolTips use fade or slide animation.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETTOOLTIPANIMATION = 0x1016,

                /// <summary>
                /// <b>Sets ToolTip animation if enabled or not.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to 1 to enable ToolTip animation or 0 to disable it. If enabled, you can use SETTOOLTIPFADE to specify fade or slide animation.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETTOOLTIPANIMATION = 0x1017,

                /// <summary>
                /// <b>If SETTOOLTIPANIMATION is enabled, GETTOOLTIPFADE indicates whether ToolTip animation uses a fade effect or a slide effect.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for fade animation or FALSE for slide animation.
                /// <br></br> • For more information on slide and fade effects, see AnimateWindow.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETTOOLTIPFADE = 0x1018,

                /// <summary>
                /// <b>If the SETTOOLTIPANIMATION flag is enabled, use SETTOOLTIPFADE to indicate whether ToolTip animation uses a fade effect or a slide effect.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE for fade animation or FALSE for slide animation. The tooltip fade effect is possible only if the system has a color depth of more than 256 colors.
                /// <br></br> • For more information on the slide and fade effects, see the AnimateWindow function.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETTOOLTIPFADE = 0x1019,


                /// <summary>
                /// <b>Determines whether native User menus have flat menu appearance.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that returns TRUE if the flat menu appearance is set, or FALSE otherwise.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETFLATMENU = 0x1022,

                /// <summary>
                /// <b>Enables or disables flat menu appearance for native User menus.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE to enable flat menu appearance or FALSE to disable it.
                /// <br></br> • When enabled, the menu bar uses COLOR_MENUBAR for the menubar background, COLOR_MENU for the menu-popup background, COLOR_MENUHILIGHT for the fill of the current menu selection, and COLOR_HILIGHT for the outline of the current menu selection.
                /// <br></br> • If disabled, menus are drawn using the same metrics and colors as in Windows 2000 and earlier.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETFLATMENU = 0x1023,

                /// <summary>
                /// <b>Determines whether the drop shadow effect is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that returns TRUE if enabled or FALSE if disabled.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETDROPSHADOW = 0x1024,

                /// <summary>
                /// <b>Enables or disables the drop shadow effect.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to 1 to enable the drop shadow effect or 0 to disable it.
                /// <br></br> • You must also have CS_DROPSHADOW in the window class style.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETDROPSHADOW = 0x1025,

                /// <summary>
                /// <b>Determines whether UI effects are enabled or disabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if all UI effects are enabled, or FALSE if they are disabled.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETUIEFFECTS = 0x103E,

                /// <summary>
                /// <b>Enables or disables UI effects.</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter to 1 to enable all UI effects or 0 to disable all UI effects.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETUIEFFECTS = 0x103F,

                /// <summary>
                /// <b>Sets dragging of full windows either on or off.</b>
                /// <br></br>
                /// <br></br> • The uiParam parameter specifies TRUE for on, or FALSE for off.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                /// </summary>
                SETDRAGFULLWINDOWS = 0x25,

                /// <summary>
                /// <b>Determines whether dragging of full windows is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled, Or FALSE otherwise.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                /// </summary>
                GETDRAGFULLWINDOWS = 0x26,

                /// <summary>
                /// <b>Determines whether menu access keys are always underlined.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if menu access keys are always underlined, And FALSE if they are underlined only when the menu Is activated by the keyboard.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETMENUUNDERLINES = 0x100A,

                /// <summary>
                /// <b>Determines whether menu access keys are always underlined.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE to always underline menu access keys, Or FALSE to underline menu access keys only when the menu Is activated from the keyboard.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETMENUUNDERLINES = 0x100B,

                /// <summary>
                /// <b>Determines whether active window tracking (activating the window the mouse is on) is on or off.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for on, Or FALSE for off.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETACTIVEWINDOWTRACKING = 0x1000,

                /// <summary>
                /// <b>Sets active window tracking (activating the window the mouse is on) either on or off.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE for on or FALSE for off.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETACTIVEWINDOWTRACKING = 0x1001,

                /// <summary>
                /// <b>Retrieves the active window tracking delay, in milliseconds.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a DWORD variable that receives the time.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETACTIVEWNDTRKTIMEOUT = 0x2002,

                /// <summary>
                /// <b>Sets the active window tracking delay.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to the number of milliseconds to delay before activating the window under the mouse pointer.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95: This value is not supported.</i>
                /// </summary>
                SETACTIVEWNDTRKTIMEOUT = 0x2003,

                /// <summary>
                /// <b>Determines whether or not windows activated through active window tracking should be brought to the top.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE for on Or FALSE for off.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETACTIVEWNDTRKZORDER = 0x100C,

                /// <summary>
                /// <b>Sets if windows activated through active window tracking should be brought to the top.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE for on Or FALSE for off.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETACTIVEWNDTRKZORDER = 0x100D,

                /// <summary>
                /// <b>Retrieves the caret width in edit controls, in pixels.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a DWORD that receives this value.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETCARETWIDTH = 0x2006,

                /// <summary>
                /// <b>Sets the caret width in edit controls.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to the desired width, in pixels.
                /// <br></br> • The default and minimum value is 1.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETCARETWIDTH = 0x2007
            }

            public enum FocusRect : int
            {
                /// <summary>
                /// <b>Retrieves the width, in pixels, of the left and right edges of the focus rectangle drawn with DrawFocusRect.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a UINT.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETFOCUSBORDERWIDTH = 0x200E,

                /// <summary>
                /// <b>Sets the height of the left and right edges of the focus rectangle drawn with DrawFocusRect</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETFOCUSBORDERWIDTH = 0x200F,

                /// <summary>
                /// <b>Retrieves the height, in pixels, of the top and bottom edges of the focus rectangle drawn with DrawFocusRect.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a UINT.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETFOCUSBORDERHEIGHT = 0x2010,

                /// <summary>
                /// <b>Sets the height of the top and bottom edges of the focus rectangle drawn with DrawFocusRect to the value of the pvParam parameter.</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETFOCUSBORDERHEIGHT = 0x2011
            }

            public enum Fonts : int
            {
                /// <summary>
                /// <b>Determines whether the font smoothing feature is enabled. This feature uses font antialiasing to make font curves appear smoother by painting pixels at different gray levels.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is enabled, or FALSE if it is not.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                /// </summary>
                GETFONTSMOOTHING = 0x4A,

                /// <summary>
                /// <b>Enables or disables the font smoothing feature, which uses font antialiasing to make font curves appear smoother by painting pixels at different gray levels.</b>
                /// <br></br>
                /// <br></br> • To enable the feature, set the uiParam parameter to TRUE. To disable the feature, set uiParam to FALSE.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95: This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                /// </summary>
                SETFONTSMOOTHING = 0x4B
            }
        }

        [Flags]
        public enum SPIF : int
        {
            ///
            None = 0x0,

            /// <summary>
            /// Writes the new system-wide parameter setting to the user profile.
            /// </summary>
            UpdateINIFile = 0x1,

            /// <summary>
            /// Broadcasts the WM_SETTINGCHANGE message after updating the user profile, but it is temporary until you logoff.
            /// </summary>
            SendChange = 0x2,

            /// <summary>
            /// Same as SENDCHANGE
            /// <br></br> Broadcasts the WM_SETTINGCHANGE message after updating the user profile, but it is temporary until you logoff.
            /// </summary>
            SendWinINIChange = SendChange
        }

    }
}
