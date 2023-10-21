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
        /// <param name="className">ClassName is the name of the associated class (eg "WinPaletter.ThemeFile")</param>
        /// <param name="description">Textual description (eg "WinPaletter ThemeFile")</param>
        /// <param name="exeProgram">ExeProgram is the app that manages that extension (eg. Assembly.GetExecutingAssembly().Location)</param>
        public static void CreateFileAssociation(string extension, string className, string description, string iconPath, string exeProgram)
        {

            if (extension.Substring(0, 1) != ".")
                extension = "." + extension;

            if (exeProgram.Contains("\""))
                exeProgram = exeProgram.Replace("\"", "");

            exeProgram = string.Format("\"{0}\"", exeProgram);

            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{extension}", "", className, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}", "", description, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\Shell\\Open", "Icon", exeProgram.Replace("\"", "") + ", 0", RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\Shell\\Open\\Command", "", exeProgram + " \"%1\"", RegistryValueKind.String);

            if ((className.ToLower() ?? "") == ("WinPaletter.ThemeFile".ToLower() ?? ""))
            {
                EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\Shell\\Edit In WinPaletter\\Command", "", exeProgram + "  /edit:\"%1\"", RegistryValueKind.String);
                EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\Shell\\Apply by WinPaletter\\Command", "", exeProgram + "  /apply:\"%1\"", RegistryValueKind.String);
            }

            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\DefaultIcon", "", iconPath, RegistryValueKind.String);

            EditReg($"HKEY_CURRENT_USER\\Software\\WinPaletter", "DisplayName", Application.ProductName, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\WinPaletter", "Publisher", Application.CompanyName, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\WinPaletter", "Version", Version, RegistryValueKind.String);

            // Notify Windows that file associations have changed
            NativeMethods.Shell32.SHChangeNotify(NativeMethods.Shell32.SHCNE_ASSOCCHANGED, NativeMethods.Shell32.SHCNF_IDLIST, 0, 0);
        }

        /// <summary>
        /// Removes WinPaletter Files Types Associate From Registry
        /// </summary>
        /// <param name="extension">Extension is the file type to be removed (eg ".wpth")</param>
        /// <param name="className">ClassName is the name of the associated class to be removed (eg "WinPaletter.ThemeFile")</param>
        public static void DeleteFileAssociation(string extension, string className)
        {

            if (extension.Substring(0, 1) != ".")
                extension = "." + extension;

            RegistryKey mainKey, descriptionKey;
            mainKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
            descriptionKey = Registry.CurrentUser.OpenSubKey(@"Software\WinPaletter", true);

            try
            {
                mainKey.DeleteSubKeyTree(extension, false);
                mainKey.DeleteSubKeyTree(className, false);

                descriptionKey.DeleteValue("DisplayName", false);
                descriptionKey.DeleteValue("Publisher", false);
                descriptionKey.DeleteValue("Version", false);
            }

            catch (Exception e)
            {
            }
            finally
            {
                if (mainKey is not null)
                    mainKey.Close();
                if (descriptionKey is not null)
                    descriptionKey.Close();
            }

            // Notify Windows that file associations have changed
            NativeMethods.Shell32.SHChangeNotify(NativeMethods.Shell32.SHCNE_ASSOCCHANGED, NativeMethods.Shell32.SHCNF_IDLIST, 0, 0);
        }
        #endregion
    }
}
