using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinPaletter.NativeMethods
{
    public class Shlwapi
    {
        private const int S_OK = 0;

        public enum AssocStr
        {
            Executable = 2,
            FriendlyDocName = 4,
            FriendlyAppName = 8,
            NoOpen = 11
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int AssocQueryString(AssocF flags, AssocStr str, string pszAssoc, string pszExtra, [Out] StringBuilder pszOut, ref uint pcchOut);

        [Flags]
        public enum AssocF : uint
        {
            None = 0,
        }

        public static string GetFriendlyTypeName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return Program.Lang.Strings.Extensions.File;

            string ext = System.IO.Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(ext)) return Program.Lang.Strings.Extensions.File;

            uint length = 0;
            AssocQueryString(AssocF.None, AssocStr.FriendlyDocName, ext, null, null, ref length);
            if (length == 0) return Program.Lang.Strings.Extensions.File;

            StringBuilder sb = new((int)length);
            int ret = AssocQueryString(AssocF.None, AssocStr.FriendlyDocName, ext, null, sb, ref length);

            return ret == S_OK ? sb.ToString() : Program.Lang.Strings.Extensions.File;
        }
    }
}
