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
        /// <summary>
        /// Registry view to be used in the class
        /// </summary>
        private static readonly RegistryView regView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default;

        /// <summary>
        /// Registry scopes
        /// </summary>
        private enum RegScope
        {
            HKEY_CURRENT_USER,
            HKEY_LOCAL_MACHINE,
            HKEY_USERS,
            HKEY_CLASSES_ROOT,
            HKEY_CURRENT_CONFIG
        }

        /// <summary>
        /// Return registry scope and key without scope
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return registry scope and key without scope for Command Prompt
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Open root registry key, and if it is HKEY_CURRENT_USER, HKEY_USERS\{SID} will be used instead. {SID} is the SID of the selected user.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Determine if the registry value can be skipped when the values are the same to avoid unnecessary registry writes and save time.
        /// </summary>
        /// <param name="existingValue"></param>
        /// <param name="targetValue"></param>
        /// <param name="regType"></param>
        /// <returns></returns>
        private static bool CanSkip(object existingValue, object targetValue, RegistryValueKind regType = RegistryValueKind.DWord)
        {
            bool skip = existingValue is null && targetValue is null;

            // Trying to convert and compare the values to avoid unnecessary registry writes and save time.
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

        /// <summary>
        /// Add verbose item to theme log if the verbose level is set to detailed, to show what is being added to the registry.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="skipped"></param>
        /// <param name="Key"></param>
        /// <param name="valueName"></param>
        /// <param name="Value"></param>
        /// <param name="RegType"></param>
        private static void AddVerboseItem(TreeView treeView, bool skipped, string Key, string valueName, object Value, RegistryValueKind RegType)
        {
            if (treeView is null)
                return;

            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string valueNameLog = valueName;
                string valueLog;
                string details;
                string imageKey;

                // Processing log text format
                if (Value is bool)
                {
                    valueLog = (Conversions.ToBoolean(Value) ? 1 : 0).ToString();
                }
                else if (Value is byte[] v)
                {
                    valueLog = string.Join(" ", v);
                }
                else
                {
                    valueLog = Value.ToString();
                }
                if (string.IsNullOrWhiteSpace(valueNameLog))
                    valueNameLog = "(default)";
                if (string.IsNullOrWhiteSpace(valueLog))
                    valueLog = "null";
                if (!skipped)
                {
                    details = string.Format(Program.Lang.Strings.ThemeManager.Advanced.RegAdd, Key, valueNameLog, valueLog, RegType.ToString());
                    imageKey = "reg_add";
                }
                else
                {
                    if (!Program.Settings.ThemeLog.ShowSkippedItemsOnDetailedVerbose)
                        return;
                    details = string.Format(Program.Lang.Strings.ThemeManager.Advanced.RegSkipped, string.Format(Program.Lang.Strings.ThemeManager.Advanced.RegAdd, Key, valueNameLog, valueLog, RegType.ToString()));
                    imageKey = "reg_skip";
                }
                ThemeLog.AddNode(treeView, details, imageKey);
            }
        }

        /// <summary>
        /// Add verbose item to theme log if the verbose level is set to detailed, to show which value being deleted from the registry.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="Key"></param>
        /// <param name="ValueName"></param>
        private static void AddVerboseItem_DelValue(TreeView treeView, string Key, string ValueName)
        {
            if (treeView is null)
                return;

            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string v0 = ValueName;

                if (string.IsNullOrWhiteSpace(v0))
                    v0 = "(default)";

                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.RegDelete, $"{Key}: {v0}"), "reg_delete");
            }
        }

        /// <summary>
        /// Add verbose item to theme log if the verbose level is set to detailed, to show which key being deleted from the registry.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="Key"></param>
        private static void AddVerboseItem_DelKey(TreeView treeView, string Key)
        {
            if (treeView is null)
                return;

            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.RegDelete, Key), "reg_delete");
            }
        }

        /// <summary>
        /// Add an exception to the theme log if an exception occurs while doing a registry process.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="ex"></param>
        /// <param name="Key"></param>
        /// <param name="ValueName"></param>
        /// <param name="Value"></param>
        /// <param name="RegType"></param>
        private static void AddVerboseException(TreeView treeView, Exception ex, string Key, string ValueName, object Value, RegistryValueKind RegType)
        {
            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string valueNameLog = ValueName;
                string valueLog;
                if (Value is bool)
                {
                    valueLog = (Conversions.ToBoolean(Value) ? 1 : 0).ToString();
                }
                else if (Value is byte[] v)
                {
                    valueLog = string.Join(" ", v);
                }
                else
                {
                    valueLog = Convert.ToString(Value);
                }

                if (string.IsNullOrWhiteSpace(valueNameLog)) valueNameLog = "(default)";
                if (string.IsNullOrWhiteSpace(valueLog)) valueLog = "null";

                string details = $"{ex.Message} - CMD: {string.Format(Program.Lang.Strings.ThemeManager.Advanced.RegAdd, Key, valueNameLog, valueLog, RegType.ToString())}";
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {details}", "error");
                Exceptions.ThemeApply.Add(new Tuple<string, Exception>(details, ex));
            }
            else
            {
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {ex.Message}", "error");
                Exceptions.ThemeApply.Add(new Tuple<string, Exception>(ex.Message, ex));
            }

            Program.Log?.Write(Serilog.Events.LogEventLevel.Error, ex, $"Exception: {ex}");
        }

        /// <summary>
        /// Edit registry, show it in theme log, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="treeView">TreeView used as a theme log</param>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        /// <param name="Value">Value</param>
        /// <param name="RegType">Kind of value to be edited to avoid errors exceptions</param>
        public static void EditReg(TreeView treeView, string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord)
        {
            EditReg(Key, ValueName, Value, RegType, treeView);
        }

        /// <summary>
        /// Edit registry, show it in theme log with using elevated Command Prompt to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="treeView">TreeView used as a theme log</param>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        /// <param name="Value">Value</param>
        /// <param name="RegType">Kind of value to be edited to avoid errors</param>
        public static void EditReg_CMD(TreeView treeView, string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord)
        {
            EditReg_CMD(Key, ValueName, Value, RegType, treeView);
        }

        /// <summary>
        /// Edit registry, show it in theme log, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        /// <param name="Value">Value</param>
        /// <param name="RegType">Kind of value to be edited to avoid errors exceptions</param>
        /// <param name="treeView">TreeView used as a theme log</param>
        public static void EditReg(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord, TreeView treeView = null)
        {
            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Process the key to get the scope and key without the scope.
            (string, RegScope) item = ProcessKey(Key_BeforeModification);

            // Key without the scope.
            Key = item.Item1;

            // Scope of the key.
            RegScope scope = item.Item2;

            // Open the processed key
            RegistryKey R = OpenBaseKey(scope);

            // If the value is null, set it to string.Empty to avoid errors.
            if (RegType == RegistryValueKind.String & Value is null) Value = string.Empty;

            // Create the key if it doesn't exist.
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
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"(EditReg skipped) `{Key_BeforeModification}` > `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}`, existing value `{existingValue}` with value type `{RegType}`");
                AddVerboseItem(treeView, true, Key_BeforeModification, ValueName, Value, RegType);
                return;
            }

            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"(EditReg) `{Key_BeforeModification}` > `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}`, new value `{Value}` with value type `{RegType}`");

            // Set the value to the registry
            try
            {
                // If the program is running as an administrator and the scope is not HKEY_LOCAL_MACHINE or HKEY_USERS, set the value directly.
                // If the program is not running as an administrator and the scope is HKEY_LOCAL_MACHINE or HKEY_USERS, use the EditReg_CMD function to try to solve security access issues.
                if (Program.Elevated && (scope == RegScope.HKEY_LOCAL_MACHINE || scope == RegScope.HKEY_USERS) || !(scope == RegScope.HKEY_LOCAL_MACHINE) & !(scope == RegScope.HKEY_USERS))
                {
                    using (RegistryKey subKey = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        subKey.SetValue(ValueName, Value, RegType);
                    }
                    AddVerboseItem(treeView, false, Key_BeforeModification, ValueName, Value, RegType);
                }
                else if (scope == RegScope.HKEY_LOCAL_MACHINE) { EditReg_CMD(treeView, $@"HKEY_LOCAL_MACHINE\{Key}", ValueName, Value, RegType); }

                else if (scope == RegScope.HKEY_USERS) { EditReg_CMD(treeView, $@"HKEY_USERS\{Key}", ValueName, Value, RegType); }
            }
            catch (SecurityException @PermissionEx)
            {
                // A security exception occurred while trying to set the value directly. Try to use the EditReg_CMD function to solve the security access issues.

                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Security exception: {@PermissionEx.Message}");

                try { EditReg_CMD(treeView, Key_BeforeModification, ValueName, Value, RegType); }
                catch { AddVerboseException(treeView, @PermissionEx, Key, ValueName, Value, RegType); }
            }
            catch (UnauthorizedAccessException @UnauthorizedAccessEx)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Unauthorized access exception: {@UnauthorizedAccessEx.Message}");

                // An unauthorized access exception occurred while trying to set the value directly. Try to use the EditReg_CMD function to solve the security access issues.
                try { EditReg_CMD(treeView, Key_BeforeModification, ValueName, Value, RegType); }
                catch { AddVerboseException(treeView, @UnauthorizedAccessEx, Key, ValueName, Value, RegType); }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Exception: {ex.Message}");

                // An exception occurred while trying to set the value directly. Try to use the EditReg_CMD function to solve the security access issues.
                try { EditReg_CMD(treeView, Key_BeforeModification, ValueName, Value, RegType); }
                catch { AddVerboseException(treeView, ex, Key, ValueName, Value, RegType); }
            }

            // Clean up the registry key
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
        /// <param name="treeView">TreeView used as a theme log</param>
        public static void EditReg_CMD(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.DWord, TreeView treeView = null)
        {
            string regTemplate;

            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;

            // Process the key to be valid for Command Prompt.
            Key = ProcessKey_CMD(Key);

            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Setting value `{ValueName}` to `{Value}` in `{Key_BeforeModification}` by using REG.EXE instead of native .NET Framework methods.");

            string _Value;

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
                            _Value = Conversions.ToInteger(Value).ToStringDWord();
                            break;
                        }

                    case RegistryValueKind.QWord:
                        {
                            regTemplate = "add \"{0}\" /v \"{1}\" /t REG_QWORD /d {2} /f";
                            _Value = Conversions.ToInteger(Value).ToStringQWord();
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
                            _Value = $@"{Value.ToString().Replace("\r\n", @"\0")}\0\0";
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
                // If the value is null, set it to string.Empty to avoid errors.
                regTemplate = "add \"{0}\" /v \"{1}\" /d \"{2}\" /f";
                _Value = string.Empty;
            }

            // Replace % with ^% to avoid errors in Command Prompt.
            if (_Value.Contains("%"))
                _Value = _Value.Replace("%", "^%");

            try
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"The sent command is `reg {string.Format(regTemplate, Key, ValueName, _Value)}`");

                Program.SendCommand($"reg {string.Format(regTemplate, Key, ValueName, _Value)}");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Exception: {ex.Message}");
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Registry edit couldn't be done by the two methods; .NET Framework methods and REG.EXE");

                AddVerboseException(treeView, ex, Key, ValueName, Value, RegType);
            }
            finally
            {
                AddVerboseItem(treeView, false, $"CMD: {Key_BeforeModification}", ValueName, Value, RegType);
            }
        }

        /// <summary>
        /// Get registry, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        /// <param name="ValueName">Name of value to be edited</param>
        /// <param name="DefaultValue">Default value that is predicted to be returned</param>
        /// <param name="RaiseExceptions">Hide execption error dialog if something wrong happened</param>
        /// <param name="IfNullReturnDefaultValue">Return 'DefaultValue' if nothing found (null) in 'Key\ValueName'</param>
        /// <returns></returns>
        public static object GetReg(string Key, string ValueName, object DefaultValue, bool RaiseExceptions = false, bool IfNullReturnDefaultValue = true)
        {
            object Result = null;
            RegistryKey R = null;

            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Redirection to HKEY_CURRENT_USER (selected user in WinPaletter)
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

            // Redirection to the actual HKEY_CURRENT_USER not HKEY_USERS\{SID} of selected user
            // There is no HKEY_REAL_CURRENT_USER, but it is used only in WinPaletter to distinguish the real current user from the selected user of the opened WinPaletter.
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

            // Redirection to HKEY_LOCAL_MACHINE with 64-bit view if the system is 64-bit, otherwise with default view (To write to the correct registry).
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

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"(GetReg) `{Key_BeforeModification}` > `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}` returned `{(IfNullReturnDefaultValue && Result is null ? DefaultValue : Result)}`");

                return IfNullReturnDefaultValue && Result is null ? DefaultValue : Result;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, ex, $"Exception: {ex.Message}");

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
            string[] Result = [];
            RegistryKey R = null;

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Redirection to HKEY_CURRENT_USER (selected user in WinPaletter)
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

            // Redirection to the actual HKEY_CURRENT_USER not HKEY_USERS\{SID} of selected user
            // There is no HKEY_REAL_CURRENT_USER, but it is used only in WinPaletter to distinguish the real current user from the selected user of the opened WinPaletter.
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

            // Redirection to HKEY_LOCAL_MACHINE with 64-bit view if the system is 64-bit, otherwise with default view (To write to the correct registry).
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
                    if (subKey is not null)
                    {
                        Result = subKey?.GetValueNames();
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"GetValueNames({Key_BeforeModification}) returned `{string.Join(", ", Result)}`");
                    }
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
            string[] Result = [];
            RegistryKey R = null;

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Redirection to HKEY_CURRENT_USER (selected user in WinPaletter)
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

            // Redirection to the actual HKEY_CURRENT_USER not HKEY_USERS\{SID} of selected user
            // There is no HKEY_REAL_CURRENT_USER, but it is used only in WinPaletter to distinguish the real current user from the selected user of the opened WinPaletter.
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

            // Redirection to HKEY_LOCAL_MACHINE with 64-bit view if the system is 64-bit, otherwise with default view (To write to the correct registry).
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
                    if (subKey is not null)
                    {
                        Result = subKey?.GetSubKeyNames();
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"GetSubKeys({Key_BeforeModification}) `{string.Join(", ", Result)}`");
                    }
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
        /// <param name="treeView"></param>
        /// <param name="deleteSubKeysAndValuesOnly">Delete only subkeys and values</param>
        public static void DelKey(string Key, bool deleteSubKeysAndValuesOnly = false, TreeView treeView = null)
        {
            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Process the key to get the scope and key without the scope.
            (string, RegScope) item = ProcessKey(Key_BeforeModification);

            // Key without the scope.
            Key = item.Item1;

            // Scope of the key.
            RegScope scope = item.Item2;

            // Open the processed key
            RegistryKey R = OpenBaseKey(scope);

            try
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Deleting registry key: {Key_BeforeModification}");
                R.DeleteSubKeyTree(Key, true);
                if (deleteSubKeysAndValuesOnly)
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Keeping the key intact and empty without subkeys and values.");
                    R.CreateSubKey(Key, true);
                }
                AddVerboseItem_DelKey(treeView, Key_BeforeModification);
            }
            catch
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Couldn't delete key using .NET Framework methods. WinPaletter will use REG.EXE");
                // An exception occurred while trying to delete the key. Try to use the DelKey_AdministratorDeflector function to solve the security access issues.
                DelKey_AdministratorDeflector(Key);
                AddVerboseItem_DelKey(treeView, Key_BeforeModification);
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
        /// <param name="treeView"></param>
        /// <param name="Key"></param>
        /// <param name="deleteSubKeysAndValuesOnly">Delete only subkeys and values</param>
        public static void DelKey(TreeView treeView, string Key, bool deleteSubKeysAndValuesOnly = false)
        {
            DelKey(Key, deleteSubKeysAndValuesOnly, treeView);
        }

        /// <summary>
        /// Delete registry key, show it in theme log, handles execptions and finally dispose the used Microsoft.Win32.RegistryKey to free up memory.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="treeView"></param>
        /// <param name="ValueName">Name of value to be edited</param>
        public static void DelValue(string Key, string ValueName, TreeView treeView = null)
        {
            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Process the key to get the scope and key without the scope.
            (string, RegScope) item = ProcessKey(Key_BeforeModification);

            // Key without the scope.
            Key = item.Item1;

            // Scope of the key.
            RegScope scope = item.Item2;

            // Open the processed key
            RegistryKey R = OpenBaseKey(scope);

            try
            {
                using (RegistryKey subR = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"(Registry DelValue) `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}` from `{Key_BeforeModification}`.");
                    subR?.DeleteValue(ValueName, true);
                    subR?.Close();
                    AddVerboseItem_DelValue(treeView, Key_BeforeModification, ValueName);
                }
            }
            catch
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Couldn't delete value using .NET Framework methods. WinPaletter will use REG.EXE");

                // An exception occurred while trying to delete the value. Try to use the DelValue_AdministratorDeflector function to solve the security access issues.
                DelValue_AdministratorDeflector(Key, ValueName);
                AddVerboseItem_DelValue(treeView, Key_BeforeModification, ValueName);
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
        /// <param name="treeView"></param>
        /// <param name="Key"></param>
        /// <param name="ValueName">Name of value to be edited</param>
        public static void DelValue(TreeView treeView, string Key, string ValueName)
        {
            DelValue(Key, ValueName, treeView);
        }

        /// <summary>
        /// Check if registry key exists
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static bool RegKeyExists(string Key)
        {
            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Process the key to get the scope and key without the scope.
            (string, RegScope) item = ProcessKey(Key_BeforeModification);

            // Key without the scope.
            Key = item.Item1;

            // Scope of the key.
            RegScope scope = item.Item2;

            // Open the processed key
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
            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Length);

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Process the key to get the scope and key without the scope.
            (string, RegScope) item = ProcessKey(Key_BeforeModification);

            // Key without the scope.
            Key = item.Item1;

            // Scope of the key.
            RegScope scope = item.Item2;

            // Open the processed key
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
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Deleting registry value using REG.EXE: reg {$@"delete ""{ProcessKey_CMD(Key)}\{ValueName}"" /f"}");

            // /f = Disable prompt
            Program.SendCommand($"reg {$@"delete ""{ProcessKey_CMD(Key)}\{ValueName}"" /f"}");
        }

        /// <summary>
        /// Delete registry key using elevated Command Prompt to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="Key">Full path of registry key. It must start by HKEY_xxxx_xxxx</param>
        public static void DelKey_AdministratorDeflector(string Key)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Deleting registry key using REG.EXE: reg {$@"delete ""{ProcessKey_CMD(Key)}"" /f"}");

            // /f = Disable prompt
            Program.SendCommand($"reg {$@"delete ""{ProcessKey_CMD(Key)}"" /f"}");
        }

        /// <summary>
        /// System File Checker (Windows tool that fixes system files)
        /// </summary>
        /// <param name="File">Target system File (it can be left "" if you want a full system scan with setting 'IfNotExist_DoScannow = true;'</param>
        /// <param name="IfNotExist_DoScannow">If 'File' doesn't exist, do a full system scan.</param>
        /// <param name="Hide">Hide console output</param>
        public static void SFC(string File = "", bool IfNotExist_DoScannow = false, bool Hide = true)
        {
            if (System.IO.File.Exists($"{SysPaths.System32}\\cmd.exe"))
            {
                IntPtr intPtr = IntPtr.Zero;

                // Disable the file system redirection to use sfc inside the System32 folder.
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
                        // Add && pause to keep the console open after the process is done if Hide is false.
                        process.StartInfo.Arguments = $"/c sfc.exe /SCANFILE=\"{File}\"{(!Hide ? " && pause" : string.Empty)}";
                    }
                    else if (IfNotExist_DoScannow)
                    {
                        // Add && pause to keep the console open after the process is done if Hide is false.
                        process.StartInfo.Arguments = $"/c sfc.exe /scannow{(!Hide ? " && pause" : string.Empty)}";
                    }
                    else
                    {
                        // Restore the file system redirection.
                        Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
                        return;
                    }

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Starting SFC scan for file: {File}");
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"The command is: {process.StartInfo.Arguments}");

                    // Start the process and wait for it to finish.
                    process.Start();
                    process.WaitForExit();

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"SFC scan finished for file: {File}");
                }

                // Restore the file system redirection.
                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
            }
        }

        /// <summary>
        /// Take ownership of a File (to current user) using an elevated Command Prompt (Takeown) to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="File">Target File</param>
        /// <param name="AsAdministrator">Take ownership to administrator instead of current user</param>
        public static void TakeOwn_File(string File, bool AsAdministrator = false)
        {
            if (System.IO.File.Exists(File))
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Taking ownership of file: {File}");
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"The command is: {SysPaths.TakeOwn} {string.Format("/f \"{0}\"", File, AsAdministrator ? " /a" : string.Empty)}");
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"TakeOwn as Administrator: {AsAdministrator}");

                Program.SendCommand($"{SysPaths.TakeOwn} {string.Format("/f \"{0}\"", File, AsAdministrator ? " /a" : string.Empty)}");

                // Try to set the access control to the current user.
                try
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Setting access control to the current user using .NET Framework methods too: {File}");

                    FileSecurity fSecurity = System.IO.File.GetAccessControl(File);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(User.Identity.Name, FileSystemRights.FullControl, AccessControlType.Allow));
                    System.IO.File.SetAccessControl(File, fSecurity);
                }
                catch (Exception ex) // Couldn't set the access control.
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Couldn't set the access control using .NET Framework methods.");

                    Forms.BugReport.ThrowError(ex);
                }
            }
            else
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"The file doesn't exist: {File}, so the ownership can't be taken.");
            }
        }

        /// <summary>
        /// Take ownership of a File (to current user) using elevated Command Prompt (ICACLS) to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="File">Target File</param>
        /// <param name="AsAdministrator">Take ownership to administrator instead of current user</param>
        public static void ICACLS(string File, bool AsAdministrator = false)
        {
            if (System.IO.File.Exists(File))
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Taking ownership of file: {File}");
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"The command is: {SysPaths.System32}\\ICACLS.exe {$"\"{File}\" /grant {(AsAdministrator ? "administrators" : "%username%")}:F"}");
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"ICACLS as Administrator: {AsAdministrator}");

                Program.SendCommand($"{SysPaths.System32}\\ICACLS.exe {$"\"{File}\" /grant {(AsAdministrator ? "administrators" : "%username%")}:F"}");

                // Try to set the access control to the current user.
                try
                {
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Setting access control to the current user using .NET Framework methods too: {File}");

                    FileSecurity fSecurity = System.IO.File.GetAccessControl(File);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(User.Identity.Name, FileSystemRights.FullControl, AccessControlType.Allow));
                    System.IO.File.SetAccessControl(File, fSecurity);
                }
                catch { }
            }
        }

        /// <summary>
        /// Move a File using elevated Command Prompt to try to solve security access issues or administrator issues.
        /// </summary>
        /// <param name="source">Target File</param>
        /// <param name="destination">Destination File</param>
        public static void Move_File(string source, string destination)
        {
            if (System.IO.File.Exists(source))
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Moving file `{source}` to `{destination}`");
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"The command is: {SysPaths.CMD} /C move \"{source}\" \"{destination}\" && exit");
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Command prompt is used to move the file to try to solve security access issues or administrator issues.");

                Program.SendCommand($"{SysPaths.CMD} /C move \"{source}\" \"{destination}\" && exit");
            }
        }
    }
}