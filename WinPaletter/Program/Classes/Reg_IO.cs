using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Security.AccessControl;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{

    /// <summary>
    /// Class contains custom Registry and IO functions
    /// </summary>
    public class Reg_IO
    {
        private enum Reg_scope
        {
            HKEY_CURRENT_USER,
            HKEY_LOCAL_MACHINE,
            HKEY_USERS,
            HKEY_CLASSES_ROOT,
            HKEY_CURRENT_CONFIG
        }

        /// <summary>
        /// Edit registry, show it in theme log, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="TreeView">TreeView used as a theme log</param>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        /// <param name="Value">Value</param>
        /// <param name="RegType">Kind of value to be edited to avoid errors exceptions</param>
        public static void EditReg(TreeView TreeView, string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord)
        {
            EditReg(Key, ValueName, Value, RegType, TreeView);
        }

        /// <summary>
        /// Edit registry, show it in theme log with using elevated Command Prompt to try to solve security access issues or Administrator issues.
        /// </summary>
        /// <param name="TreeView">TreeView used as a theme log</param>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        /// <param name="Value">Value</param>
        /// <param name="RegType">Kind of value to be edited to avoid errors</param>
        public static void EditReg_CMD(TreeView TreeView, string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord)
        {
            EditReg_CMD(Key, ValueName, Value, RegType, TreeView);
        }

        /// <summary>
        /// Edit registry, show it in theme log, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        /// <param name="Value">Value</param>
        /// <param name="RegType">Kind of value to be edited to avoid errors exceptions</param>
        /// <param name="TreeView">TreeView used as a theme log</param>
        public static void EditReg(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord, TreeView TreeView = null)
        {
            RegistryKey R = null;

            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase))
                Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;

            if (RegType == RegistryValueKind.String & Value is null)
                Value = string.Empty;

            Reg_scope scope = default(Reg_scope);

            if (Key.StartsWith("HKEY_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
            {
                scope = Reg_scope.HKEY_CURRENT_USER;
                Key = Key.Remove(0, @"HKEY_CURRENT_USER\".Count());
            }

            else if (Key.StartsWith("HKEY_USERS", StringComparison.OrdinalIgnoreCase))
            {
                scope = Reg_scope.HKEY_USERS;
                Key = Key.Remove(0, @"HKEY_USERS\".Count());
            }

            else if (Key.StartsWith("HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
            {
                scope = Reg_scope.HKEY_LOCAL_MACHINE;
                Key = Key.Remove(0, @"HKEY_LOCAL_MACHINE\".Count());
            }

            else if (Key.StartsWith("HKEY_CLASSES_ROOT", StringComparison.OrdinalIgnoreCase))
            {
                scope = Reg_scope.HKEY_CLASSES_ROOT;
                Key = Key.Remove(0, @"HKEY_CLASSES_ROOT\".Count());
            }

            else if (Key.StartsWith("HKEY_CURRENT_CONFIG", StringComparison.OrdinalIgnoreCase))
            {
                scope = Reg_scope.HKEY_CURRENT_CONFIG;
                Key = Key.Remove(0, @"HKEY_CURRENT_CONFIG\".Count());
            }

            switch (scope)
            {
                case Reg_scope.HKEY_CURRENT_USER:
                    {
                        try
                        {
                            R = User.SID != User.AdminSID_GrantedUAC
                                ? RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32).OpenSubKey(User.SID)
                                : RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                        }
                        catch
                        {
                            R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                        }
                        break;
                    }

                case Reg_scope.HKEY_CURRENT_CONFIG:
                    {
                        R = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry32);
                        break;
                    }

                case Reg_scope.HKEY_CLASSES_ROOT:
                    {
                        R = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32);
                        break;
                    }

                case Reg_scope.HKEY_LOCAL_MACHINE:
                    {
                        R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default);
                        break;
                    }

                case Reg_scope.HKEY_USERS:
                    {
                        R = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32);
                        break;
                    }
            }

            try
            {
                if (R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree) is null)
                    R.CreateSubKey(Key, true);
            }
            catch { }

            // Skips setting to registry if the values are the same
            object ToCheck = GetReg(Key_BeforeModification, ValueName, null);
            object CheckBy = Value;
            bool Skip = false;
            if (ToCheck is not null)
            {
                switch (RegType)
                {
                    case RegistryValueKind.MultiString:
                        {
                            if (ToCheck.GetType().IsArray)
                            {
                                try
                                {
                                    Skip = Enumerable.SequenceEqual((string[])ToCheck, (string[])CheckBy);
                                }
                                catch { }
                            }
                            break;
                        }

                    case RegistryValueKind.Binary:
                        {
                            if (ToCheck.GetType().IsArray)
                            {
                                try
                                {
                                    Skip = Enumerable.SequenceEqual((byte[])ToCheck, (byte[])CheckBy);
                                }
                                catch { }
                            }
                            break;
                        }

                    case RegistryValueKind.DWord: // int
                        {
                            if (CheckBy is not bool)
                            {
                                try
                                {
                                    int conversion_0 = Convert.ToInt32(ToCheck);
                                    int conversion_1 = Convert.ToInt32(CheckBy);
                                    Skip = conversion_0.Equals(conversion_1);
                                }
                                catch { }
                            }
                            else
                            {
                                try
                                {
                                    int conversion_0 = Convert.ToInt32(ToCheck);
                                    int conversion_1 = Convert.ToBoolean(CheckBy) ? 1 : 0;
                                    Skip = conversion_0.Equals(conversion_1);
                                }
                                catch { }

                            }
                            break;
                        }

                    case RegistryValueKind.QWord: // ulong
                        {
                            if (CheckBy is not bool)
                            {
                                try
                                {
                                    ulong conversion_0 = Convert.ToUInt64(ToCheck);
                                    ulong conversion_1 = Convert.ToUInt64(CheckBy);
                                    Skip = conversion_0.Equals(conversion_1);
                                }
                                catch { }
                            }
                            else
                            {
                                try
                                {
                                    ulong conversion_0 = Convert.ToUInt64(ToCheck);
                                    ulong conversion_1 = Convert.ToBoolean(CheckBy) ? 1u : 0u;
                                    Skip = conversion_0.Equals(conversion_1);
                                }
                                catch { }
                            }
                            break;
                        }

                    case RegistryValueKind.String: // string
                        {
                            try
                            {
                                Skip = ToCheck.ToString().Equals(CheckBy.ToString());
                            }
                            catch { }
                            break;
                        }

                    case RegistryValueKind.ExpandString: // string
                        {
                            try
                            {
                                Skip = ToCheck.ToString().Equals(CheckBy.ToString());
                            }
                            catch { }
                            break;
                        }

                    default:
                        {
                            try
                            {
                                Skip = ToCheck.Equals(CheckBy);
                            }
                            catch { }
                            break;
                        }
                }

                if (Skip)
                {
                    AddVerboseItem(TreeView, true, Key_BeforeModification, ValueName, Value, RegType);
                    return;
                }
            }

            try
            {
                if (Program.Elevated && (scope == Reg_scope.HKEY_LOCAL_MACHINE || scope == Reg_scope.HKEY_USERS) || !(scope == Reg_scope.HKEY_LOCAL_MACHINE) & !(scope == Reg_scope.HKEY_USERS))
                {
                    R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue(ValueName, Value, RegType);
                    AddVerboseItem(TreeView, false, Key_BeforeModification, ValueName, Value, RegType);
                }
                else if (scope == Reg_scope.HKEY_LOCAL_MACHINE) { EditReg_CMD(TreeView, $@"HKEY_LOCAL_MACHINE\{Key}", ValueName, Value, RegType); }

                else if (scope == Reg_scope.HKEY_USERS) { EditReg_CMD(TreeView, $@"HKEY_USERS\{Key}", ValueName, Value, RegType); }
            }
            catch (SecurityException @PermissionEx)
            {
                try { EditReg_CMD(TreeView, Key_BeforeModification, ValueName, Value, RegType); }
                catch { AddVerboseException(TreeView, @PermissionEx, Key, ValueName, Value, RegType); }
            }
            catch (UnauthorizedAccessException @UnauthorizedAccessEx)
            {
                try { EditReg_CMD(TreeView, Key_BeforeModification, ValueName, Value, RegType); }
                catch { AddVerboseException(TreeView, @UnauthorizedAccessEx, Key, ValueName, Value, RegType); }
            }
            catch (Exception ex)
            {
                try { EditReg_CMD(TreeView, Key_BeforeModification, ValueName, Value, RegType); }
                catch { AddVerboseException(TreeView, ex, Key, ValueName, Value, RegType); }
            }

            try
            {
                if (R is not null)
                {
                    R.Flush();
                    R.Close();
                    R.Dispose();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Edit registry, show it in theme log with using elevated Command Prompt to try to solve security access issues or Administrator issues.
        /// </summary>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        /// <param name="Value">Value</param>
        /// <param name="RegType">Kind of value to be edited to avoid errors</param>
        /// <param name="TreeView">TreeView used as a theme log</param>
        public static void EditReg_CMD(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord, TreeView TreeView = null)
        {
            string regTemplate;

            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase))
                Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;

            string _Value;

            if (Key.StartsWith("HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
                Key = $"HKLM{(Key.Remove(0, "HKEY_LOCAL_MACHINE".Count()))}";

            if (Key.StartsWith("HKEY_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
                Key = $"{((User.SID != User.AdminSID_GrantedUAC ? $"HKU\\{User.SID}" : "HKCU"))}{(Key.Remove(0, "HKEY_CURRENT_USER".Count()))}";

            if (Key.StartsWith("HKEY_USERS", StringComparison.OrdinalIgnoreCase))
                Key = $"HKU{(Key.Remove(0, "HKEY_USERS".Count()))}";

            if (Key.StartsWith("HKEY_CLASSES_ROOT", StringComparison.OrdinalIgnoreCase))
                Key = $"HKCR{(Key.Remove(0, "HKEY_CLASSES_ROOT".Count()))}";

            if (Key.StartsWith("HKEY_CURRENT_CONFIG", StringComparison.OrdinalIgnoreCase))
                Key = $"HKCC{(Key.Remove(0, "HKEY_CURRENT_CONFIG".Count()))}";

            // /v = Value Name
            // /t = Registry Value Type
            // /d = Value
            // /f = Disable prompt
            if (Value is not null)
            {
                switch (RegType)
                {
                    case RegistryValueKind.String:
                        {
                            regTemplate = "add \"{0}\" /v \"{1}\" /t REG_SZ /d \"{2}\" /f";
                            _Value = Value.ToString();
                            break;
                        }

                    case RegistryValueKind.DWord:
                        {
                            regTemplate = "add \"{0}\" /v \"{1}\" /t REG_DWORD /d {2} /f";
                            _Value = Conversions.ToInteger(Value).DWORD();
                            break;
                        }

                    case RegistryValueKind.QWord:
                        {
                            regTemplate = "add \"{0}\" /v \"{1}\" /t REG_QWORD /d {2} /f";
                            _Value = Conversions.ToInteger(Value).QWORD();
                            break;
                        }

                    case RegistryValueKind.Binary:
                        {
                            regTemplate = "add \"{0}\" /v \"{1}\" /t REG_BINARY /d {2} /f";
                            _Value = BitConverter.ToString((byte[])Value).Replace("-", string.Empty);
                            break;
                        }

                    case RegistryValueKind.ExpandString:
                        {
                            regTemplate = "add \"{0}\" /v \"{1}\" /t REG_EXPAND_SZ /d \"{2}\" /f";
                            _Value = Value.ToString();
                            break;
                        }

                    case RegistryValueKind.MultiString:
                        {
                            regTemplate = "add \"{0}\" /v \"{1}\" /t REG_MULTI_SZ /d \"{2}\" /f";
                            _Value = $@"{(Value.ToString().Replace("\r\n", @"\0"))}\0\0";
                            break;
                        }
                    // A sequence of null-terminated strings, terminated by an empty string (\0). The following is an example: String1\0String2\0String3\0LastString\0\0. The first \0 terminates the first string, the second-from-last \0 terminates the last string, and the final \0 terminates the sequence. Note that the final terminator must be factored into the length of the string.

                    case RegistryValueKind.None:
                        {
                            regTemplate = "add \"{0}\" /v \"{1}\" /t REG_NONE /d \"{2}\" /f";
                            _Value = Value.ToString();
                            break;
                        }

                    default:
                        {
                            regTemplate = "add \"{0}\" /v \"{1}\" /d \"{2}\" /f";
                            _Value = Value.ToString();
                            break;
                        }

                }
            }

            else
            {
                regTemplate = "add \"{0}\" /v \"{1}\" /d \"{2}\" /f";
                _Value = string.Empty;

            }

            if (_Value.Contains("%"))
                _Value = _Value.Replace("%", "^%");

            try
            {
                Program.SendCommand($"reg {string.Format(regTemplate, Key, ValueName, _Value)}");
            }
            catch (Exception ex)
            {
                AddVerboseException(TreeView, ex, Key, ValueName, Value, RegType);
            }
            finally
            {
                AddVerboseItem(TreeView, false, $"CMD: {Key_BeforeModification}", ValueName, Value, RegType);
            }

        }

        private static void AddVerboseItem(TreeView TreeView, bool Skipped, string Key, string ValueName, object Value, RegistryValueKind RegType)
        {
            if (TreeView is null)
                return;

            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string v0 = ValueName;
                string v1;
                string v2;
                string v3;
                if (Value is bool)
                {
                    v1 = (Conversions.ToBoolean(Value) ? 1 : 0).ToString();
                }
                else if (Value is byte[])
                {
                    v1 = string.Join(" ", (byte[])Value);
                }
                else
                {
                    v1 = Value.ToString();
                }
                if (string.IsNullOrWhiteSpace(v0))
                    v0 = "(default)";
                if (string.IsNullOrWhiteSpace(v1))
                    v1 = "null";
                if (!Skipped)
                {
                    v2 = string.Format(Program.Lang.Verbose_RegAdd, Key, v0, v1, RegType.ToString());
                    v3 = "reg_add";
                }
                else
                {
                    if (!Program.Settings.ThemeLog.ShowSkippedItemsOnDetailedVerbose)
                        return;
                    v2 = string.Format(Program.Lang.Verbose_RegSkipped, string.Format(Program.Lang.Verbose_RegAdd, Key, v0, v1, RegType.ToString()));
                    v3 = "reg_skip";
                }
                Theme.Manager.AddNode(TreeView, v2, v3);
            }
        }

        private static void AddVerboseException(TreeView TreeView, Exception ex, string Key, string ValueName, object Value, RegistryValueKind RegType)
        {
            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string v0 = ValueName;
                string v1;
                if (Value is bool)
                {
                    v1 = (Conversions.ToBoolean(Value) ? 1 : 0).ToString();
                }
                else if (Value is byte[])
                {
                    v1 = string.Join(" ", (byte[])Value);
                }
                else
                {
                    v1 = Convert.ToString(Value);
                }
                if (string.IsNullOrWhiteSpace(v0))
                    v0 = "(default)";
                if (string.IsNullOrWhiteSpace(v1))
                    v1 = "null";
                string v2 = $"{ex.Message} - CMD: {(string.Format(Program.Lang.Verbose_RegAdd, Key, v0, v1, RegType.ToString()))}";
                if (TreeView is not null)
                    Theme.Manager.AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {v2}", "error");
                Exceptions.ThemeApply.Add(new Tuple<string, Exception>(v2, ex));
            }
            else
            {
                if (TreeView is not null)
                    Theme.Manager.AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {ex.Message}", "error");
                Exceptions.ThemeApply.Add(new Tuple<string, Exception>(ex.Message, ex));
            }
        }

        /// <summary>
        /// Get registry, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        /// <param name="DefaultValue">Default value that is predicted to be returned</param>
        /// <param name="RaiseExceptions">Show execption error dialog if something wrong happened</param>
        /// <param name="IfNullReturnDefaultValue">Return 'DefaultValue' if nothing found (null) in 'Key\ValueName'</param>
        /// <returns></returns>
        public static object GetReg(string Key, string ValueName, object DefaultValue, bool RaiseExceptions = false, bool IfNullReturnDefaultValue = true)
        {
            object Result = null;
            RegistryKey R = null;

            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase))
                Key = Key.Remove(0, @"Computer\".Count());

            if (Key.StartsWith("HKEY_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CURRENT_USER\".Count());
                try
                {
                    R = User.SID != User.AdminSID_GrantedUAC
                        ? RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32).OpenSubKey(User.SID)
                        : RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                }
                catch
                {
                    R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                }
            }

            //Deflection to HKEY_CURRENT_USER (that opened WinPaletter not the real current user) if value starts with #USR:
            else if (Key.StartsWith("HKEY_REAL_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_REAL_CURRENT_USER\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            }

            else if (Key.StartsWith("HKEY_USERS", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_USERS\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32);
            }

            else if (Key.StartsWith("HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_LOCAL_MACHINE\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default);
            }

            else if (Key.StartsWith("HKEY_CLASSES_ROOT", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CLASSES_ROOT\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32);
            }

            else if (Key.StartsWith("HKEY_CURRENT_CONFIG", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CURRENT_CONFIG\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry32);
            }

            try
            {
                if (R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey) is not null)
                    Result = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey).GetValue(ValueName, DefaultValue);
                try
                {
                    if (R is not null)
                    {
                        R.Flush();
                        R.Close();
                        R.Dispose();
                    }
                }
                catch { }

                if (Result != null && Result.ToString().StartsWith("#USR:", StringComparison.OrdinalIgnoreCase))
                {
                    Result = GetReg($"HKEY_REAL_CURRENT_USER\\{Result.ToString().Replace("#USR:", string.Empty)}", ValueName, DefaultValue, RaiseExceptions, IfNullReturnDefaultValue);
                }

                return IfNullReturnDefaultValue && Result is null ? DefaultValue : Result;

            }
            catch (Exception ex)
            {
                Exceptions.ThemeLoad.Add(new Tuple<string, Exception>($"{Key} : {ValueName}", ex));
                if (RaiseExceptions)
                    Forms.BugReport.ThrowError(ex);
                try
                {
                    if (R is not null)
                    {
                        R.Flush();
                        R.Close();
                        R.Dispose();
                    }
                }
                catch
                {
                }
                return DefaultValue;
            }
        }

        /// <summary>
        /// Delete registry value using elevated Command Prompt to try to solve security access issues or Administrator issues.
        /// </summary>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        public static void DelReg_AdministratorDeflector(string Key, string ValueName)
        {
            string regTemplate;
            if (Key.StartsWith("HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
                Key = $"HKLM{(Key.Remove(0, "HKEY_LOCAL_MACHINE".Count()))}";

            if (Key.StartsWith("HKEY_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
                Key = $"{((User.SID != User.AdminSID_GrantedUAC ? $"HKU\\{User.SID}" : "HKCU"))}{(Key.Remove(0, "HKEY_CURRENT_USER".Count()))}";

            if (Key.StartsWith("HKEY_USERS", StringComparison.OrdinalIgnoreCase))
                Key = $"HKU{(Key.Remove(0, "HKEY_USERS".Count()))}";

            if (Key.StartsWith("HKEY_CLASSES_ROOT", StringComparison.OrdinalIgnoreCase))
                Key = $"HKCR{(Key.Remove(0, "HKEY_CLASSES_ROOT".Count()))}";

            if (Key.StartsWith("HKEY_CURRENT_CONFIG", StringComparison.OrdinalIgnoreCase))
                Key = $"HKCC{(Key.Remove(0, "HKEY_CURRENT_CONFIG".Count()))}";

            // /f = Disable prompt
            regTemplate = @"delete ""{0}\{1}"" /f";
            Program.SendCommand($"reg {string.Format(regTemplate, Key, ValueName)}");
        }

        /// <summary>
        /// System File Checker (Windows tool that fixes system files)
        /// </summary>
        /// <param name="File">Target system file (it can be left "" if you want a full system scan with setting 'IfNotExist_DoScannow = true;'</param>
        /// <param name="IfNotExist_DoScannow">If 'File' doesn't exist, do a full system scan.</param>
        /// <param name="Hide">Hide console output</param>
        public static void SFC(string File = "", bool IfNotExist_DoScannow = false, bool Hide = true)
        {
            if (OS.WXP)
                return;

            IntPtr intPtr = IntPtr.Zero;
            Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);

            using (Process process = new()
            {
                StartInfo = new()
                {
                    FileName = $"{PathsExt.System32}\\cmd.exe",
                    Verb = "runas",
                    WindowStyle = Hide ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal,
                    CreateNoWindow = Hide,
                    UseShellExecute = true
                }
            })
            {

                if (System.IO.File.Exists(File))
                {
                    process.StartInfo.Arguments = $"/c sfc.exe /SCANFILE=\"{File}\"{(!Hide ? " && pause" : string.Empty)}";
                }
                else if (IfNotExist_DoScannow)
                {
                    process.StartInfo.Arguments = $"/c sfc.exe /scannow{(!Hide ? " && pause" : string.Empty)}";
                }
                else
                {
                    Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
                    return;
                }

                process.Start();
                process.WaitForExit();
            }

            Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
        }

        /// <summary>
        /// Take ownership of a file (to current user) using elevated Command Prompt (Takeown) to try to solve security access issues or Administrator issues.
        /// </summary>
        /// <param name="File">Target file</param>
        /// <param name="AsAdministrator">Take ownership to Administrator instead of current user</param>
        public static void Takeown_File(string File, bool AsAdministrator = false)
        {
            if (System.IO.File.Exists(File))
            {
                Program.SendCommand($"{PathsExt.TakeOwn} {string.Format("/f \"{0}\"", File, AsAdministrator ? " /a" : string.Empty)}");

                try
                {
                    FileSecurity fSecurity = System.IO.File.GetAccessControl(File);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow));
                    System.IO.File.SetAccessControl(File, fSecurity);
                }
                catch { }
            }
        }

        /// <summary>
        /// Take ownership of a file (to current user) using elevated Command Prompt (ICACLS) to try to solve security access issues or Administrator issues.
        /// </summary>
        /// <param name="File">Target file</param>
        /// <param name="AsAdministrator">Take ownership to Administrator instead of current user</param>
        public static void ICACLS(string File, bool AsAdministrator = false)
        {
            if (System.IO.File.Exists(File))
            {
                Program.SendCommand($"{PathsExt.System32}\\ICACLS.exe {$"\"{File}\" /grant {(AsAdministrator ? "administrators" : "%username%")}:F"}");

                try
                {
                    FileSecurity fSecurity = System.IO.File.GetAccessControl(File);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow));
                    System.IO.File.SetAccessControl(File, fSecurity);
                }
                catch { }
            }
        }

        /// <summary>
        /// Move a file using elevated Command Prompt to try to solve security access issues or Administrator issues.
        /// </summary>
        /// <param name="source">Target file</param>
        /// <param name="destination">Destination file</param>
        public static void Move_File(string source, string destination)
        {
            if (System.IO.File.Exists(source)) { Program.SendCommand($"{PathsExt.CMD} /C move \"{source}\" \"{destination}\" && exit"); }
        }
    }
}