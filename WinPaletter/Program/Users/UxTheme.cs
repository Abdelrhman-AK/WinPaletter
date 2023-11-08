using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using WinPaletter.Theme;

namespace WinPaletter.NativeMethods
{
    public partial class UxTheme
    {
        private partial class PrivateFunctions
        {
            [DllImport("UxTheme.DLL", BestFitMapping = false, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, EntryPoint = "#65")]
            public static extern int SetSystemVisualStyle(string pszFilename, string pszColor, string pszSize, int dwReserved);

            [DllImport("uxtheme", ExactSpelling = true)]
            public static extern int EnableTheming(int fEnable);

            [DllImport("uxtheme", CharSet = CharSet.Unicode)]
            public static extern int GetCurrentThemeName(StringBuilder stringThemeName, int lengthThemeName, StringBuilder stringColorName, int lengthColorName, StringBuilder stringSizeName, int lengthSizeName);
        }


        public static bool SetSystemVisualStyle(string pszFilename, string pszColor, string pszSize, int dwReserved, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.SetSystemVisualStyle(pszFilename, pszColor, pszSize, dwReserved) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "UxTheme", pszFilename, pszColor, pszSize, 0, result), "dll");

                wic.Undo();
                return result;
            }
        }

        public static bool SetSystemVisualStyle(TreeView TreeView, string pszFilename, string pszColor, string pszSize, int dwReserved)
        {
            return SetSystemVisualStyle(pszFilename, pszColor, pszSize, dwReserved, TreeView);
        }


        public static bool EnableTheming(int fEnable, TreeView TreeView = null)
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                bool result = false;
                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    result = PrivateFunctions.EnableTheming(fEnable) == 1;
                    advapi.RevertToSelf();
                }

                if (TreeView != null)
                    Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", fEnable, result), "dll");

                wic.Undo();
                return result;
            }
        }

        public static bool EnableTheming(TreeView TreeView, int fEnable)
        {
            return EnableTheming(fEnable, TreeView);
        }


        /// <summary>
        /// Get current applied visual styles data
        /// </summary>
        /// <returns>
        /// <code>Item1: Theme file
        /// Item2: Color name
        /// Item3: Size name
        /// </code></returns>
        public static Tuple<string, string, string> GetCurrentVS()
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                StringBuilder vsFile = new(260);
                StringBuilder colorName = new(260);
                StringBuilder sizeName = new(260);

                if (User.SID == User.AdminSID_GrantedUAC || advapi.ImpersonateLoggedOnUser(User.Token))
                {
                    PrivateFunctions.GetCurrentThemeName(vsFile, vsFile.Capacity, colorName, colorName.Capacity, sizeName, sizeName.Capacity);
                    advapi.RevertToSelf();
                }

                wic.Undo();
                return new Tuple<string, string, string>(vsFile.ToString(), colorName.ToString(), sizeName.ToString());
            }
        }
    }
}
