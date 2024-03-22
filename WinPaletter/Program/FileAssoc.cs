using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program
    {
        #region File Association And Uninstall

        /// <summary>
        /// Associate WinPaletter Files Types in Registry
        /// </summary>
        /// <param name="extension">Extension is the file type to be registered (eg ".wpth")</param>
        /// <param name="className">ClassName is the name of the associated class (eg "WinPaletter.WindowsXPThemePath")</param>
        /// <param name="description">Textual description (eg "WinPaletter WindowsXPThemePath")</param>
        /// <param name="exeProgram">ExeProgram is the app that manages that extension (eg. Assembly.GetExecutingAssembly().Location)</param>
        public static bool CreateFileAssociation(string extension, string className, string description, string iconPath, string exeProgram)
        {
            if (extension.Substring(0, 1) != ".") extension = $".{extension}";

            if (exeProgram.Contains("\"")) exeProgram = exeProgram.Replace("\"", string.Empty);

            exeProgram = $"\"{exeProgram}\"";

            bool isInstalledBefore = GetReg($"HKEY_CURRENT_USER\\Software\\Classes\\{extension}", string.Empty, null) != null;

            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{extension}", string.Empty, className, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}", string.Empty, description, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\Shell\\Open", "Icon", $"{(exeProgram.Replace("\"", string.Empty))}, 0", RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\Shell\\Open\\Command", string.Empty, $"{exeProgram} \"%1\"", RegistryValueKind.String);

            if ((className.ToLower() ?? string.Empty) == ("WinPaletter.ThemeFile".ToLower() ?? string.Empty))
            {
                EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\Shell\\Edit In WinPaletter\\Command", string.Empty, $"{exeProgram}  -e \"%1\"", RegistryValueKind.String);
                EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\Shell\\Apply by WinPaletter\\Command", string.Empty, $"{exeProgram}  -a \"%1\"", RegistryValueKind.String);
            }

            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\DefaultIcon", string.Empty, iconPath, RegistryValueKind.String);

            EditReg($"HKEY_CURRENT_USER\\Software\\WinPaletter", "DisplayName", Application.ProductName, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\WinPaletter", "Publisher", Application.CompanyName, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\WinPaletter", "Version", Version, RegistryValueKind.String);

            return isInstalledBefore;
        }

        /// <summary>
        /// Removes WinPaletter Files Types Associate From Registry
        /// </summary>
        /// <param name="extension">Extension is the file type to be removed (eg ".wpth")</param>
        /// <param name="className">ClassName is the name of the associated class to be removed (eg "WinPaletter.WindowsXPThemePath")</param>
        public static void DeleteFileAssociation(string extension, string className)
        {
            if (extension.Substring(0, 1) != ".") extension = $".{extension}";

            DelKey($"HKEY_CURRENT_USER\\Software\\Classes\\{extension}");
            DelKey($"HKEY_CURRENT_USER\\Software\\Classes\\{className}");
            DelValue("HKEY_CURRENT_USER\\Software\\WinPaletter", "DisplayName");
            DelValue("HKEY_CURRENT_USER\\Software\\WinPaletter", "Publisher");
            DelValue("HKEY_CURRENT_USER\\Software\\WinPaletter", "Version");

            // Notify Windows that file associations have changed
            NativeMethods.Shell32.SHChangeNotify(NativeMethods.Shell32.ShellConstants.SHCNE_ASSOCCHANGED, NativeMethods.Shell32.ShellConstants.SHCNF_IDLIST, 0, 0);
        }
        #endregion
    }
}
