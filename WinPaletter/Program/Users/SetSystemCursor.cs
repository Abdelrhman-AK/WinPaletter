using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace WinPaletter.NativeMethods
{
    public partial class User32
    {
        private partial class PrivateFunctions
        {
            [DllImport("user32.dll")]
            public static extern bool SetSystemCursor(IntPtr hcur, int id);
        }

        public static bool SetSystemCursor(string file, OCR_SYSTEM_CURSORS id, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SetSystemCursor(LoadCursorFromFile(file), (int)id);
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", file, id, result), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SetSystemCursor(TreeView TreeView, string file, OCR_SYSTEM_CURSORS id)
        {
            return SetSystemCursor(file, id, TreeView);
        }

        public static bool SetSystemCursor(IntPtr hcur, int id, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SetSystemCursor(hcur, id);
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", hcur.ToInt32(), OCR_SYSTEM_CURSORS.OCR_APPSTARTING.ToString()), "dll");

                impersonationContext.Undo();
                return result;
            }
        }
        public static bool SetSystemCursor(TreeView TreeView, IntPtr hcur, int id)
        {
            return SetSystemCursor(hcur, id, TreeView);
        }
    }
}
