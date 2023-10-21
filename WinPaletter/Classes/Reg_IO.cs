using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
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

        public static void EditReg(TreeView TreeView, string KeyName, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord)
        {
            EditReg(KeyName, ValueName, Value, RegType, TreeView);
        }

        public static void EditReg_CMD(TreeView TreeView, string KeyName, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord)
        {
            EditReg_CMD(KeyName, ValueName, Value, RegType, TreeView);
        }

        public static void EditReg(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord, TreeView TreeView = null)
        {
            RegistryKey R = null;

            if (Key.StartsWith(@"Computer\", (StringComparison)5))
                Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;

            if (RegType == RegistryValueKind.String & Value is null)
                Value = "";

            var scope = default(Reg_scope);

            if (Key.StartsWith("HKEY_CURRENT_USER", (StringComparison)5))
            {
                scope = Reg_scope.HKEY_CURRENT_USER;
                Key = Key.Remove(0, @"HKEY_CURRENT_USER\".Count());
            }

            else if (Key.StartsWith("HKEY_USERS", (StringComparison)5))
            {
                scope = Reg_scope.HKEY_USERS;
                Key = Key.Remove(0, @"HKEY_USERS\".Count());
            }

            else if (Key.StartsWith("HKEY_LOCAL_MACHINE", (StringComparison)5))
            {
                scope = Reg_scope.HKEY_LOCAL_MACHINE;
                Key = Key.Remove(0, @"HKEY_LOCAL_MACHINE\".Count());
            }

            else if (Key.StartsWith("HKEY_CLASSES_ROOT", (StringComparison)5))
            {
                scope = Reg_scope.HKEY_CLASSES_ROOT;
                Key = Key.Remove(0, @"HKEY_CLASSES_ROOT\".Count());
            }

            else if (Key.StartsWith("HKEY_CURRENT_CONFIG", (StringComparison)5))
            {
                scope = Reg_scope.HKEY_CURRENT_CONFIG;
                Key = Key.Remove(0, @"HKEY_CURRENT_CONFIG\".Count());
            }

            switch (scope)
            {
                case Reg_scope.HKEY_CURRENT_USER:
                    {
                        R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                        if (R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree) is null)
                            R.CreateSubKey(Key, true);
                        break;
                    }

                case Reg_scope.HKEY_CURRENT_CONFIG:
                    {
                        R = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry32);
                        if (R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree) is null)
                            R.CreateSubKey(Key, true);
                        break;
                    }

                case Reg_scope.HKEY_CLASSES_ROOT:
                    {
                        R = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32);
                        if (R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree) is null)
                            R.CreateSubKey(Key, true);
                        break;
                    }

                case Reg_scope.HKEY_LOCAL_MACHINE:
                    {
                        R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default);
                        if (Program.Elevated)
                        {
                            if (R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree) is null)
                                R.CreateSubKey(Key, true);
                        }

                        break;
                    }

                case Reg_scope.HKEY_USERS:
                    {
                        R = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32);
                        if (Program.Elevated)
                        {
                            if (R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree) is null)
                                R.CreateSubKey(Key, true);
                        }

                        break;
                    }

            }

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
                else if (scope == Reg_scope.HKEY_LOCAL_MACHINE)
                {
                    EditReg_CMD(TreeView, @"HKEY_LOCAL_MACHINE\" + Key, ValueName, Value, RegType);
                }
                else if (scope == Reg_scope.HKEY_USERS)
                {
                    EditReg_CMD(TreeView, @"HKEY_USERS\" + Key, ValueName, Value, RegType);
                }
            }
            catch (Exception ex)
            {
                AddVerboseException(TreeView, ex, Key, ValueName, Value, RegType);
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

        public static void EditReg_CMD(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord, TreeView TreeView = null)
        {
            string regTemplate;

            if (Key.StartsWith(@"Computer\", (StringComparison)5))
                Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;

            string _Value;
            if (Key.StartsWith("HKEY_LOCAL_MACHINE", (StringComparison)5))
                Key = "HKLM" + Key.Remove(0, "HKEY_LOCAL_MACHINE".Count());
            if (Key.StartsWith("HKEY_CURRENT_USER", (StringComparison)5))
                Key = "HKCU" + Key.Remove(0, "HKEY_CURRENT_USER".Count());
            if (Key.StartsWith("HKEY_USERS", (StringComparison)5))
                Key = "HKU" + Key.Remove(0, "HKEY_USERS".Count());
            if (Key.StartsWith("HKEY_CLASSES_ROOT", (StringComparison)5))
                Key = "HKCR" + Key.Remove(0, "HKEY_CLASSES_ROOT".Count());
            if (Key.StartsWith("HKEY_CURRENT_CONFIG", (StringComparison)5))
                Key = "HKCC" + Key.Remove(0, "HKEY_CURRENT_CONFIG".Count());

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
                            _Value = BitConverter.ToString((byte[])Value).Replace("-", "");
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
                            _Value = Value.ToString().Replace("\r\n", @"\0") + @"\0\0";
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
                _Value = "";

            }

            if (_Value.Contains("%"))
                _Value = _Value.Replace("%", "^%");

            try
            {
                Program.CMD_Wrapper.SendCommand($"reg {string.Format(regTemplate, Key, ValueName, _Value)}");
            }
            catch (Exception ex)
            {
                AddVerboseException(TreeView, ex, Key, ValueName, Value, RegType);
            }
            finally
            {
                AddVerboseItem(TreeView, false, "CMD: " + Key_BeforeModification, ValueName, Value, RegType);
            }

        }

        private static void AddVerboseItem(TreeView TreeView, bool Skipped, string Key, string ValueName, object Value, RegistryValueKind RegType)
        {
            if (TreeView is null)
                return;
            if (Program.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string v0 = ValueName;
                string v1;
                string v2;
                string v3;
                if (Value is bool)
                {
                    v1 = Conversions.ToBoolean(Value).ToInteger().ToString();
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
            if (Program.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string v0 = ValueName;
                string v1;
                if (Value is bool)
                {
                    v1 = Conversions.ToBoolean(Value).ToInteger().ToString();
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
                string v2 = ex.Message + " - " + "CMD: " + string.Format(Program.Lang.Verbose_RegAdd, Key, v0, v1, RegType.ToString());
                if (TreeView is not null)
                    Theme.Manager.AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), v2), "error");
                Exceptions.ThemeApply.Add(new Tuple<string, Exception>(v2, ex));
            }
            else
            {
                if (TreeView is not null)
                    Theme.Manager.AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), ex.Message), "error");
                Exceptions.ThemeApply.Add(new Tuple<string, Exception>(ex.Message, ex));
            }
        }

        public static object GetReg(string KeyName, string ValueName, object DefaultValue, bool RaiseExceptions = false, bool IfNothingReturnDefaultValue = true)
        {
            object Result = null;
            RegistryKey R = null;

            if (KeyName.StartsWith(@"Computer\", (StringComparison)5))
                KeyName = KeyName.Remove(0, @"Computer\".Count());

            if (KeyName.StartsWith("HKEY_CURRENT_USER", (StringComparison)5))
            {
                KeyName = KeyName.Remove(0, @"HKEY_CURRENT_USER\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            }

            else if (KeyName.StartsWith("HKEY_USERS", (StringComparison)5))
            {
                KeyName = KeyName.Remove(0, @"HKEY_USERS\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32);
            }

            else if (KeyName.StartsWith("HKEY_LOCAL_MACHINE", (StringComparison)5))
            {
                KeyName = KeyName.Remove(0, @"HKEY_LOCAL_MACHINE\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default);
            }

            else if (KeyName.StartsWith("HKEY_CLASSES_ROOT", (StringComparison)5))
            {
                KeyName = KeyName.Remove(0, @"HKEY_CLASSES_ROOT\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32);
            }

            else if (KeyName.StartsWith("HKEY_CURRENT_CONFIG", (StringComparison)5))
            {
                KeyName = KeyName.Remove(0, @"HKEY_CURRENT_CONFIG\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry32);

            }

            try
            {
                if (R.OpenSubKey(KeyName, (RegistryKeyPermissionCheck)Conversions.ToInteger(false), RegistryRights.ReadKey) is not null)
                    Result = R.OpenSubKey(KeyName, (RegistryKeyPermissionCheck)Conversions.ToInteger(false), RegistryRights.ReadKey).GetValue(ValueName, DefaultValue);
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
                return IfNothingReturnDefaultValue && Result is null ? DefaultValue : Result;
            }
            catch (Exception ex)
            {
                Exceptions.ThemeLoad.Add(new Tuple<string, Exception>(KeyName + " : " + ValueName, ex));
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
        public static void DelReg_AdministratorDeflector(string RegistryKeyPath, string ValueName)
        {
            string regTemplate;
            if (RegistryKeyPath.StartsWith("HKEY_LOCAL_MACHINE", (StringComparison)5))
                RegistryKeyPath = "HKLM" + RegistryKeyPath.Remove(0, "HKEY_LOCAL_MACHINE".Count());
            if (RegistryKeyPath.StartsWith("HKEY_CURRENT_USER", (StringComparison)5))
                RegistryKeyPath = "HKCU" + RegistryKeyPath.Remove(0, "HKEY_CURRENT_USER".Count());
            if (RegistryKeyPath.StartsWith("HKEY_USERS", (StringComparison)5))
                RegistryKeyPath = "HKU" + RegistryKeyPath.Remove(0, "HKEY_USERS".Count());
            if (RegistryKeyPath.StartsWith("HKEY_CLASSES_ROOT", (StringComparison)5))
                RegistryKeyPath = "HKCR" + RegistryKeyPath.Remove(0, "HKEY_CLASSES_ROOT".Count());
            if (RegistryKeyPath.StartsWith("HKEY_CURRENT_CONFIG", (StringComparison)5))
                RegistryKeyPath = "HKCC" + RegistryKeyPath.Remove(0, "HKEY_CURRENT_CONFIG".Count());

            // /f = Disable prompt
            regTemplate = @"delete ""{0}\{1}"" /f";
            Program.CMD_Wrapper.SendCommand($"reg {string.Format(regTemplate, RegistryKeyPath, ValueName)}");
        }

        public static void SFC(string File = "", bool IfNotExist_DoScannow = false, bool Hide = true)
        {
            if (OS.WXP)
                return;

            IntPtr intPtr = IntPtr.Zero;
            Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);

            using (var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = PathsExt.System32 + "\\cmd.exe",
                    Verb = "runas",
                    WindowStyle = Hide ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal,
                    CreateNoWindow = Hide,
                    UseShellExecute = true
                }
            })
            {

                if (System.IO.File.Exists(File))
                {
                    process.StartInfo.Arguments = "/c sfc.exe /SCANFILE=\"" + File + $"\"{(!Hide ? " && pause" : "")}";
                }
                else if (IfNotExist_DoScannow)
                {
                    process.StartInfo.Arguments = $"/c sfc.exe /scannow{(!Hide ? " && pause" : "")}";
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

        public static void Takeown_File(string File, bool AsAdministrator = false)
        {
            if (System.IO.File.Exists(File))
            {
                Program.CMD_Wrapper.SendCommand($"{PathsExt.System32}\\takeown.exe {string.Format("/f \"{0}\"", File, AsAdministrator ? " /a" : "")}");

                try
                {
                    var fSecurity = System.IO.File.GetAccessControl(File);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow));
                    System.IO.File.SetAccessControl(File, fSecurity);
                }
                catch
                {
                }

            }
        }

        public static void ICACLS(string File, bool AsAdministrator = false)
        {
            if (System.IO.File.Exists(File))
            {
                Program.CMD_Wrapper.SendCommand($"{PathsExt.System32}\\ICACLS.exe {string.Format("\"{0}\" /grant {1}:F", File, AsAdministrator ? "administrators" : "%username%")}");

                try
                {
                    var fSecurity = System.IO.File.GetAccessControl(File);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow));
                    System.IO.File.SetAccessControl(File, fSecurity);
                }
                catch
                {
                }
            }
        }
        public static void Move_File(string source, string destination)
        {
            if (System.IO.File.Exists(source))
            {
                Program.CMD_Wrapper.SendCommand($"{PathsExt.CMD} {string.Format("move \"{0}\" \"{1}\"", source, destination)}");
            }
        }
    }
}