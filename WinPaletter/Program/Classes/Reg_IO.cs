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
        private static RegistryView regView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default;

        private enum RegScope
        {
            HKEY_CURRENT_USER,
            HKEY_LOCAL_MACHINE,
            HKEY_USERS,
            HKEY_CLASSES_ROOT,
            HKEY_CURRENT_CONFIG
        }

        private static (string, RegScope) ProcessKey(string Key, RegScope scope = RegScope.HKEY_CURRENT_USER)
        {
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            if (Key.StartsWith("HKEY_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_CURRENT_USER;
                Key = Key.Remove(0, @"HKEY_CURRENT_USER\".Count());
            }

            else if (Key.StartsWith("HKEY_USERS", StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_USERS;
                Key = Key.Remove(0, @"HKEY_USERS\".Count());
            }

            else if (Key.StartsWith("HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_LOCAL_MACHINE;
                Key = Key.Remove(0, @"HKEY_LOCAL_MACHINE\".Count());
            }

            else if (Key.StartsWith("HKEY_CLASSES_ROOT", StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_CLASSES_ROOT;
                Key = Key.Remove(0, @"HKEY_CLASSES_ROOT\".Count());
            }

            else if (Key.StartsWith("HKEY_CURRENT_CONFIG", StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_CURRENT_CONFIG;
                Key = Key.Remove(0, @"HKEY_CURRENT_CONFIG\".Count());
            }

            return new(Key, scope);
        }

        private static string ProcessKey_CMD(string Key)
        {
            string key = Key;

            if (key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) key = key.Remove(0, @"Computer\".Count());

            if (key.StartsWith("HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
                key = $"HKLM{key.Remove(0, "HKEY_LOCAL_MACHINE".Count())}";

            if (key.StartsWith("HKEY_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
                key = $"{(User.SID != User.AdminSID_GrantedUAC ? $"HKU\\{User.SID}" : "HKCU")}{key.Remove(0, "HKEY_CURRENT_USER".Count())}";

            if (key.StartsWith("HKEY_USERS", StringComparison.OrdinalIgnoreCase))
                key = $"HKU{key.Remove(0, "HKEY_USERS".Count())}";

            if (key.StartsWith("HKEY_CLASSES_ROOT", StringComparison.OrdinalIgnoreCase))
                key = $"HKCR{key.Remove(0, "HKEY_CLASSES_ROOT".Count())}";

            if (key.StartsWith("HKEY_CURRENT_CONFIG", StringComparison.OrdinalIgnoreCase))
                key = $"HKCC{key.Remove(0, "HKEY_CURRENT_CONFIG".Count())}";

            return key;
        }

        private static RegistryKey OpenBaseKey(RegScope scope)
        {
            switch (scope)
            {
                case RegScope.HKEY_CURRENT_USER:
                    {
                        try
                        {
                            return User.SID != User.AdminSID_GrantedUAC
                                ? RegistryKey.OpenBaseKey(RegistryHive.Users, regView).OpenSubKey(User.SID)
                                : RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
                        }
                        catch
                        {
                            return RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
                        }
                    }

                case RegScope.HKEY_CURRENT_CONFIG:
                    {
                        return RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, regView);
                    }

                case RegScope.HKEY_CLASSES_ROOT:
                    {
                        return RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, regView);
                    }

                case RegScope.HKEY_LOCAL_MACHINE:
                    {
                        return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, regView);
                    }

                case RegScope.HKEY_USERS:
                    {
                        return RegistryKey.OpenBaseKey(RegistryHive.Users, regView);
                    }
                default:
                    {
                        {
                            try
                            {
                                return User.SID != User.AdminSID_GrantedUAC
                                    ? RegistryKey.OpenBaseKey(RegistryHive.Users, regView).OpenSubKey(User.SID)
                                    : RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
                            }
                            catch
                            {
                                return RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
                            }
                        }
                    }
            }
        }

        private static bool CanSkip(object existingValue, object targetValue, RegistryValueKind regType = RegistryValueKind.DWord)
        {
            bool skip = existingValue is null && targetValue is null;

            if (existingValue is not null)
            {
                switch (regType)
                {
                    case RegistryValueKind.MultiString:
                        {
                            if (existingValue.GetType().IsArray)
                            {
                                try
                                {
                                    skip = Enumerable.SequenceEqual((string[])existingValue, (string[])targetValue);
                                }
                                catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.
                            }
                            break;
                        }

                    case RegistryValueKind.Binary:
                        {
                            if (existingValue.GetType().IsArray)
                            {
                                try
                                {
                                    skip = Enumerable.SequenceEqual((byte[])existingValue, (byte[])targetValue);
                                }
                                catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.
                            }
                            break;
                        }

                    case RegistryValueKind.DWord: // int
                        {
                            if (targetValue is not bool)
                            {
                                try
                                {
                                    int conversion_0 = Convert.ToInt32(existingValue);
                                    int conversion_1 = Convert.ToInt32(targetValue);
                                    skip = conversion_0.Equals(conversion_1);
                                }
                                catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.
                            }
                            else
                            {
                                try
                                {
                                    int conversion_0 = Convert.ToInt32(existingValue);
                                    int conversion_1 = Convert.ToBoolean(targetValue) ? 1 : 0;
                                    skip = conversion_0.Equals(conversion_1);
                                }
                                catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.

                            }
                            break;
                        }

                    case RegistryValueKind.QWord: // ulong
                        {
                            if (targetValue is not bool)
                            {
                                try
                                {
                                    ulong conversion_0 = Convert.ToUInt64(existingValue);
                                    ulong conversion_1 = Convert.ToUInt64(targetValue);
                                    skip = conversion_0.Equals(conversion_1);
                                }
                                catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.
                            }
                            else
                            {
                                try
                                {
                                    ulong conversion_0 = Convert.ToUInt64(existingValue);
                                    ulong conversion_1 = Convert.ToBoolean(targetValue) ? 1u : 0u;
                                    skip = conversion_0.Equals(conversion_1);
                                }
                                catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.
                            }
                            break;
                        }

                    case RegistryValueKind.String: // string
                        {
                            try
                            {
                                skip = existingValue.ToString().Equals(targetValue.ToString());
                            }
                            catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.
                            break;
                        }

                    case RegistryValueKind.ExpandString: // string
                        {
                            try
                            {
                                skip = existingValue.ToString().Equals(targetValue.ToString());
                            }
                            catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.
                            break;
                        }

                    default:
                        {
                            try
                            {
                                skip = existingValue.Equals(targetValue);
                            }
                            catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.
                            break;
                        }
                }
            }
            return skip;
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

        private static void AddVerboseItem_DelValue(TreeView TreeView, string Key, string ValueName)
        {
            if (TreeView is null)
                return;

            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string v0 = ValueName;

                if (string.IsNullOrWhiteSpace(v0))
                    v0 = "(default)";

                Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_RegDelete, Key + ": " + v0), "reg_delete");
            }
        }

        private static void AddVerboseItem_DelKey(TreeView TreeView, string Key)
        {
            if (TreeView is null)
                return;

            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_RegDelete, Key), "reg_delete");
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
        /// Edit registry, show it in theme log with using elevated Command Prompt to try to solve security access issues or administrator issues.
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
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;

            (string, RegScope) item = ProcessKey(Key_BeforeModification);
            Key = item.Item1;
            RegScope scope = item.Item2;
            RegistryKey R = OpenBaseKey(scope);

            if (RegType == RegistryValueKind.String & Value is null) Value = string.Empty;

            try
            {
                using (RegistryKey subKey = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (subKey is null) R.CreateSubKey(Key, true);
                    subKey?.Close();
                }
            }
            catch { } // Couldn't create the key, but we will try to set the value anyway.

            // Skips setting to registry if the values are the same
            object existingValue = GetReg(Key_BeforeModification, ValueName, null);
            if (existingValue is not null && CanSkip(existingValue, Value, RegType))
            {
                AddVerboseItem(TreeView, true, Key_BeforeModification, ValueName, Value, RegType);
                return;
            }

            try
            {
                if (Program.Elevated && (scope == RegScope.HKEY_LOCAL_MACHINE || scope == RegScope.HKEY_USERS) || !(scope == RegScope.HKEY_LOCAL_MACHINE) & !(scope == RegScope.HKEY_USERS))
                {
                    using (RegistryKey subKey = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        subKey.SetValue(ValueName, Value, RegType);
                    }
                    AddVerboseItem(TreeView, false, Key_BeforeModification, ValueName, Value, RegType);
                }
                else if (scope == RegScope.HKEY_LOCAL_MACHINE) { EditReg_CMD(TreeView, $@"HKEY_LOCAL_MACHINE\{Key}", ValueName, Value, RegType); }

                else if (scope == RegScope.HKEY_USERS) { EditReg_CMD(TreeView, $@"HKEY_USERS\{Key}", ValueName, Value, RegType); }
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
                R?.Flush();
                R?.Close();
                R?.Dispose();
            }
            catch { } // Couldn't close the key.
        }

        /// <summary>
        /// Edit registry, show it in theme log with using elevated Command Prompt to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        /// <param name="Value">Value</param>
        /// <param name="RegType">Kind of value to be edited to avoid errors</param>
        /// <param name="TreeView">TreeView used as a theme log</param>
        public static void EditReg_CMD(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord, TreeView TreeView = null)
        {
            string regTemplate;

            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;
            Key = ProcessKey_CMD(Key);

            string _Value;

            // /v = Value Name
            // /t = Registry Value ButtonOverlay
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
                        ? RegistryKey.OpenBaseKey(RegistryHive.Users, regView).OpenSubKey(User.SID)
                        : RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
                }
                catch
                {
                    R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
                }
            }

            //Deflection to HKEY_CURRENT_USER (that opened WinPaletter not the real current user) if value starts with #USR:
            else if (Key.StartsWith("HKEY_REAL_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_REAL_CURRENT_USER\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
            }

            else if (Key.StartsWith("HKEY_USERS", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_USERS\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.Users, regView);
            }

            else if (Key.StartsWith("HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_LOCAL_MACHINE\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default);
            }

            else if (Key.StartsWith("HKEY_CLASSES_ROOT", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CLASSES_ROOT\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, regView);
            }

            else if (Key.StartsWith("HKEY_CURRENT_CONFIG", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CURRENT_CONFIG\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, regView);
            }

            try
            {
                using (RegistryKey subKey = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey))
                {
                    if (subKey is not null) Result = subKey?.GetValue(ValueName, DefaultValue);
                    subKey?.Close();
                }
                try
                {
                    R?.Flush();
                    R?.Close();
                    R?.Dispose();
                }
                catch { } // Couldn't close the key.

                if (Result != null && Result.ToString().StartsWith("#USR:", StringComparison.OrdinalIgnoreCase))
                {
                    Result = GetReg($"HKEY_REAL_CURRENT_USER\\{Result.ToString().Replace("#USR:", string.Empty)}", ValueName, DefaultValue, RaiseExceptions, IfNullReturnDefaultValue);
                }

                return IfNullReturnDefaultValue && Result is null ? DefaultValue : Result;
            }
            catch (Exception ex)
            {
                Exceptions.ThemeLoad.Add(new Tuple<string, Exception>($"{Key} : {ValueName}", ex));
                if (RaiseExceptions) Forms.BugReport.ThrowError(ex);
                try
                {
                    R?.Flush();
                    R?.Close();
                    R?.Dispose();
                }
                catch { } // Couldn't close the key.
                return DefaultValue;
            }
        }

        /// <summary>
        /// Get value names of registry key
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string[] GetValueNames(string Key)
        {
            string[] Result = new string[] { };
            RegistryKey R = null;

            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            if (Key.StartsWith("HKEY_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CURRENT_USER\".Count());
                try
                {
                    R = User.SID != User.AdminSID_GrantedUAC
                        ? RegistryKey.OpenBaseKey(RegistryHive.Users, regView).OpenSubKey(User.SID)
                        : RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
                }
                catch
                {
                    R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
                }
            }

            //Deflection to HKEY_CURRENT_USER (that opened WinPaletter not the real current user) if value starts with #USR:
            else if (Key.StartsWith("HKEY_REAL_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_REAL_CURRENT_USER\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
            }

            else if (Key.StartsWith("HKEY_USERS", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_USERS\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.Users, regView);
            }

            else if (Key.StartsWith("HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_LOCAL_MACHINE\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default);
            }

            else if (Key.StartsWith("HKEY_CLASSES_ROOT", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CLASSES_ROOT\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, regView);
            }

            else if (Key.StartsWith("HKEY_CURRENT_CONFIG", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CURRENT_CONFIG\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, regView);
            }

            try
            {
                using (RegistryKey subKey = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey))
                {
                    if (subKey is not null) Result = subKey?.GetValueNames();
                    subKey?.Close();
                }
            }
            catch { } // Couldn't get the value names.

            try
            {
                R?.Flush();
                R?.Close();
                R?.Dispose();
            }
            catch { } // Couldn't close the key.

            return Result;
        }

        /// <summary>
        /// Get value names of registry key
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string[] GetSubKeys(string Key)
        {
            string[] Result = new string[] { };
            RegistryKey R = null;

            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase))
                Key = Key.Remove(0, @"Computer\".Count());

            if (Key.StartsWith("HKEY_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CURRENT_USER\".Count());
                try
                {
                    R = User.SID != User.AdminSID_GrantedUAC
                        ? RegistryKey.OpenBaseKey(RegistryHive.Users, regView).OpenSubKey(User.SID)
                        : RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
                }
                catch
                {
                    R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
                }
            }

            //Deflection to HKEY_CURRENT_USER (that opened WinPaletter not the real current user) if value starts with #USR:
            else if (Key.StartsWith("HKEY_REAL_CURRENT_USER", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_REAL_CURRENT_USER\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
            }

            else if (Key.StartsWith("HKEY_USERS", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_USERS\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.Users, regView);
            }

            else if (Key.StartsWith("HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_LOCAL_MACHINE\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default);
            }

            else if (Key.StartsWith("HKEY_CLASSES_ROOT", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CLASSES_ROOT\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, regView);
            }

            else if (Key.StartsWith("HKEY_CURRENT_CONFIG", StringComparison.OrdinalIgnoreCase))
            {
                Key = Key.Remove(0, @"HKEY_CURRENT_CONFIG\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, regView);
            }

            try
            {
                using (RegistryKey subKey = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey))
                {
                    if (subKey is not null) Result = subKey?.GetSubKeyNames();
                    subKey?.Close();
                }
            }
            catch { } // Couldn't get the value names.

            try
            {
                R?.Flush();
                R?.Close();
                R?.Dispose();
            }
            catch { } // Couldn't close the key.

            return Result;
        }

        /// <summary>
        /// Delete registry key, show it in theme log, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="TreeView"></param>
        /// <param name="deleteSubKeysAndValuesOnly">Delete only subkeys and values</param>
        public static void DelKey(string Key, bool deleteSubKeysAndValuesOnly = false, TreeView TreeView = null)
        {
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;

            (string, RegScope) item = ProcessKey(Key_BeforeModification);
            Key = item.Item1;
            RegScope scope = item.Item2;
            RegistryKey R = OpenBaseKey(scope);

            try
            {
                R.DeleteSubKeyTree(Key, true);
                if (deleteSubKeysAndValuesOnly) R.CreateSubKey(Key, true);
                AddVerboseItem_DelKey(TreeView, Key_BeforeModification);
            }
            catch
            {
                DelKey_AdministratorDeflector(Key);
                AddVerboseItem_DelKey(TreeView, Key_BeforeModification);
            }
            finally
            {
                try
                {
                    R?.Flush();
                    R?.Close();
                    R?.Dispose();
                }
                catch { } // Couldn't close the key.
            }
        }

        /// <summary>
        /// Delete registry key, show it in theme log, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="TreeView"></param>
        /// <param name="Key"></param>
        /// <param name="deleteSubKeysAndValuesOnly">Delete only subkeys and values</param>
        public static void DelKey(TreeView TreeView, string Key, bool deleteSubKeysAndValuesOnly = false)
        {
            DelKey(Key, deleteSubKeysAndValuesOnly, TreeView);
        }

        /// <summary>
        /// Delete registry key, show it in theme log, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="TreeView"></param>
        /// <param name="ValueName">Name of value to be edited</param>
        public static void DelValue(string Key, string ValueName, TreeView TreeView = null)
        {
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;

            (string, RegScope) item = ProcessKey(Key_BeforeModification);
            Key = item.Item1;
            RegScope scope = item.Item2;
            RegistryKey R = OpenBaseKey(scope);

            try
            {
                using (RegistryKey subR = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    subR?.DeleteValue(ValueName, true);
                    subR?.Close();
                    AddVerboseItem_DelValue(TreeView, Key_BeforeModification, ValueName);
                }
            }
            catch
            {
                DelValue_AdministratorDeflector(Key, ValueName);
                AddVerboseItem_DelValue(TreeView, Key_BeforeModification, ValueName);
            }
            finally
            {
                try
                {
                    R?.Flush();
                    R?.Close();
                    R?.Dispose();
                }
                catch { } // Couldn't close the key.
            }
        }

        /// <summary>
        /// Delete registry key, show it in theme log, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="TreeView"></param>
        /// <param name="Key"></param>
        /// <param name="ValueName">Name of value to be edited</param>
        public static void DelValue(TreeView TreeView, string Key, string ValueName)
        {
            DelValue(Key, ValueName, TreeView);
        }

        /// <summary>
        /// Check if registry key exists
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static bool RegKeyExists(string Key)
        {
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;

            (string, RegScope) item = ProcessKey(Key_BeforeModification);
            Key = item.Item1;
            RegScope scope = item.Item2;
            RegistryKey R = OpenBaseKey(scope);

            bool exists = false;

            try
            {
                using (RegistryKey subR = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree))
                {
                    if (subR is not null) exists = true;
                    subR?.Close();
                }
            }
            catch
            {
                exists = false;
            }
            finally
            {
                try
                {
                    R?.Flush();
                    R?.Close();
                    R?.Dispose();
                }
                catch { } // Couldn't close the key.
            }

            return exists;
        }

        /// <summary>
        /// Check if registry value exists
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="ValueName"></param>
        /// <returns></returns>
        public static bool RegValueExists(string Key, string ValueName)
        {
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Length);

            string Key_BeforeModification = Key;

            (string, RegScope) item = ProcessKey(Key_BeforeModification);
            Key = item.Item1;
            RegScope scope = item.Item2;
            RegistryKey R = OpenBaseKey(scope);

            bool key_exists = false;
            bool value_exists = false;

            try
            {
                using (RegistryKey subR = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree))
                {
                    if (subR is not null) key_exists = true;

                    // If key exists, check if the value exists
                    if (key_exists)
                    {
                        object value = subR.GetValue(ValueName, null);
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            value_exists = true;
                        }
                    }

                    subR?.Close();
                }
            }
            catch
            {
                key_exists = false;
                value_exists = false;
            }
            finally
            {
                try
                {
                    R?.Flush();
                    R?.Close();
                    R?.Dispose();
                }
                catch { } // Couldn't close the key.
            }

            return value_exists;
        }

        /// <summary>
        /// Delete registry value using elevated Command Prompt to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        public static void DelValue_AdministratorDeflector(string Key, string ValueName)
        {
            // /f = Disable prompt
            Program.SendCommand($"reg {string.Format(@"delete ""{0}\{1}"" /f", ProcessKey_CMD(Key), ValueName)}");
        }

        /// <summary>
        /// Delete registry key using elevated Command Prompt to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        public static void DelKey_AdministratorDeflector(string Key)
        {
            // /f = Disable prompt
            Program.SendCommand($"reg {string.Format(@"delete ""{0}"" /f", ProcessKey_CMD(Key))}");
        }

        /// <summary>
        /// System File Checker (Windows tool that fixes system files)
        /// </summary>
        /// <param name="File">Target system file (it can be left "" if you want a full system scan with setting 'IfNotExist_DoScannow = true;'</param>
        /// <param name="IfNotExist_DoScannow">If 'File' doesn't exist, do a full system scan.</param>
        /// <param name="Hide">Hide console output</param>
        public static void SFC(string File = "", bool IfNotExist_DoScannow = false, bool Hide = true)
        {
            if (System.IO.File.Exists($"{SysPaths.System32}\\cmd.exe"))
            {
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);

                using (Process process = new()
                {
                    StartInfo = new()
                    {
                        FileName = $"{SysPaths.System32}\\cmd.exe",
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
        }

        /// <summary>
        /// Take ownership of a file (to current user) using elevated Command Prompt (Takeown) to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="File">Target file</param>
        /// <param name="AsAdministrator">Take ownership to administrator instead of current user</param>
        public static void TakeOwn_File(string File, bool AsAdministrator = false)
        {
            if (System.IO.File.Exists(File))
            {
                Program.SendCommand($"{SysPaths.TakeOwn} {string.Format("/f \"{0}\"", File, AsAdministrator ? " /a" : string.Empty)}");

                try
                {
                    FileSecurity fSecurity = System.IO.File.GetAccessControl(File);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow));
                    System.IO.File.SetAccessControl(File, fSecurity);
                }
                catch (Exception ex) // Couldn't set the access control.
                {
                    Forms.BugReport.ThrowError(ex);
                }
            }
        }

        /// <summary>
        /// Take ownership of a file (to current user) using elevated Command Prompt (ICACLS) to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="File">Target file</param>
        /// <param name="AsAdministrator">Take ownership to administrator instead of current user</param>
        public static void ICACLS(string File, bool AsAdministrator = false)
        {
            if (System.IO.File.Exists(File))
            {
                Program.SendCommand($"{SysPaths.System32}\\ICACLS.exe {$"\"{File}\" /grant {(AsAdministrator ? "administrators" : "%username%")}:F"}");

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
        /// Move a file using elevated Command Prompt to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="source">Target file</param>
        /// <param name="destination">Destination file</param>
        public static void Move_File(string source, string destination)
        {
            if (System.IO.File.Exists(source)) { Program.SendCommand($"{SysPaths.CMD} /C move \"{source}\" \"{destination}\" && exit"); }
        }
    }
}