using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.AccessControl;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// Provides utility methods for interacting with the Windows Registry, including reading, writing, deleting, and
    /// managing registry keys and values. This class also includes methods for logging registry operations and
    /// handling security or access issues.
    /// </summary>
    public class Reg_IO
    {
        private static readonly RegistryView regView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default;

        // Cache for registry hives to avoid repeated opens
        private static readonly Dictionary<RegScope, RegistryKey> _baseKeyCache = new Dictionary<RegScope, RegistryKey>();
        private static readonly object _cacheLock = new();

        // Pre-compiled format strings
        private const string ComputerPrefix = "Computer\\";
        private const string HKCUPrefix = "HKEY_CURRENT_USER\\";
        private const string HKUPrefix = "HKEY_USERS\\";
        private const string HKMLPrefix = "HKEY_LOCAL_MACHINE\\";
        private const string HKCRPrefix = "HKEY_CLASSES_ROOT\\";
        private const string HKCCPrefix = "HKEY_CURRENT_CONFIG\\";
        private const string RealHKCUPrefix = "HKEY_REAL_CURRENT_USER\\";

        // Cache for frequently accessed SID
        private static string CurrentUserSid => _currentUserSid ??= User.SID;
        private static string _currentUserSid;

        private static string AdminSid => _adminSid ??= User.AdminSID_GrantedUAC;
        private static string _adminSid;

        private enum RegScope
        {
            HKEY_CURRENT_USER,
            HKEY_LOCAL_MACHINE,
            HKEY_USERS,
            HKEY_CLASSES_ROOT,
            HKEY_CURRENT_CONFIG,
            HKEY_REAL_CURRENT_USER
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (string, RegScope) FormatKey(string Key, RegScope scope = RegScope.HKEY_CURRENT_USER)
        {
            if (string.IsNullOrEmpty(Key)) return (Key, scope);

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            if (Key.StartsWith(HKCUPrefix, StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_CURRENT_USER;
                Key = Key.Substring(HKCUPrefix.Length);
            }
            else if (Key.StartsWith(HKUPrefix, StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_USERS;
                Key = Key.Substring(HKUPrefix.Length);
            }
            else if (Key.StartsWith(HKMLPrefix, StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_LOCAL_MACHINE;
                Key = Key.Substring(HKMLPrefix.Length);
            }
            else if (Key.StartsWith(HKCRPrefix, StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_CLASSES_ROOT;
                Key = Key.Substring(HKCRPrefix.Length);
            }
            else if (Key.StartsWith(HKCCPrefix, StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_CURRENT_CONFIG;
                Key = Key.Substring(HKCCPrefix.Length);
            }
            else if (Key.StartsWith(RealHKCUPrefix, StringComparison.OrdinalIgnoreCase))
            {
                scope = RegScope.HKEY_REAL_CURRENT_USER;
                Key = Key.Substring(RealHKCUPrefix.Length);
            }

            return (Key, scope);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string FormatKey_CMD(string Key)
        {
            if (string.IsNullOrEmpty(Key)) return Key;

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            if (Key.StartsWith(HKMLPrefix, StringComparison.OrdinalIgnoreCase))
                Key = "HKLM" + Key.Substring(HKMLPrefix.Length - 1);
            else if (Key.StartsWith(HKCUPrefix, StringComparison.OrdinalIgnoreCase))
                Key = (CurrentUserSid != AdminSid ? $"HKU\\{CurrentUserSid}" : "HKCU") + Key.Substring(HKCUPrefix.Length - 1);
            else if (Key.StartsWith(HKUPrefix, StringComparison.OrdinalIgnoreCase))
                Key = "HKU" + Key.Substring(HKUPrefix.Length - 1);
            else if (Key.StartsWith(HKCRPrefix, StringComparison.OrdinalIgnoreCase))
                Key = "HKCR" + Key.Substring(HKCRPrefix.Length - 1);
            else if (Key.StartsWith(HKCCPrefix, StringComparison.OrdinalIgnoreCase))
                Key = "HKCC" + Key.Substring(HKCCPrefix.Length - 1);
            else if (Key.StartsWith(RealHKCUPrefix, StringComparison.OrdinalIgnoreCase))
                Key = "HKCU" + Key.Substring(RealHKCUPrefix.Length - 1);

            return Key;
        }

        private static RegistryKey OpenBaseKey(RegScope scope)
        {
            lock (_cacheLock)
            {
                if (_baseKeyCache.TryGetValue(scope, out var cachedKey) && cachedKey != null)
                {
                    try
                    {
                        // Verify the key is still valid
                        if (cachedKey.Handle != null) return cachedKey;
                    }
                    catch
                    {
                        _baseKeyCache.Remove(scope);
                    }
                }

                var key = OpenBaseKeyInternal(scope);
                _baseKeyCache[scope] = key;
                return key;
            }
        }

        private static RegistryKey OpenBaseKeyInternal(RegScope scope)
        {
            switch (scope)
            {
                case RegScope.HKEY_CURRENT_USER:
                    return CurrentUserSid != AdminSid ? OpenUsersSubKey(CurrentUserSid) : RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);

                case RegScope.HKEY_REAL_CURRENT_USER:
                    return RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);

                case RegScope.HKEY_CURRENT_CONFIG:
                    return RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, regView);

                case RegScope.HKEY_CLASSES_ROOT:
                    return RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, regView);

                case RegScope.HKEY_LOCAL_MACHINE:
                    return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, regView);

                case RegScope.HKEY_USERS:
                    return RegistryKey.OpenBaseKey(RegistryHive.Users, regView);

                default:
                    return CurrentUserSid != AdminSid ? OpenUsersSubKey(CurrentUserSid) : RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
            }
        }

        private static RegistryKey OpenUsersSubKey(string sid)
        {
            try
            {
                using (var usersKey = RegistryKey.OpenBaseKey(RegistryHive.Users, regView))
                {
                    return usersKey.OpenSubKey(sid);
                }
            }
            catch
            {
                return RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, regView);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CanSkip(object existingValue, object targetValue, RegistryValueKind RegType = RegistryValueKind.Unknown)
        {
            if (existingValue == null && targetValue == null) return true;
            if (existingValue == null || targetValue == null) return false;

            if (RegType == RegistryValueKind.Unknown) RegType = GetRegistryValueKind(targetValue);

            switch (RegType)
            {
                case RegistryValueKind.MultiString when existingValue is string[] a && targetValue is string[] b:
                    if (a.Length != b.Length) return false;
                    for (int i = 0; i < a.Length; i++) if (!string.Equals(a[i], b[i], StringComparison.Ordinal)) return false;
                    return true;

                case RegistryValueKind.Binary when existingValue is byte[] eb && targetValue is byte[] tb:
                    if (eb.Length != tb.Length) return false;
                    return ((ReadOnlySpan<byte>)eb).SequenceEqual(tb);

                case RegistryValueKind.DWord:
                    try
                    {
                        if (targetValue is bool b) return Convert.ToInt32(existingValue) == (b ? 1 : 0);
                        return Convert.ToInt32(existingValue) == Convert.ToInt32(targetValue);
                    }
                    catch { return false; }

                case RegistryValueKind.QWord:
                    try
                    {
                        if (targetValue is bool b) return Convert.ToUInt64(existingValue) == (b ? 1uL : 0uL);
                        return Convert.ToUInt64(existingValue) == Convert.ToUInt64(targetValue);
                    }
                    catch { return false; }

                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    return string.Equals(existingValue.ToString(), targetValue.ToString(), StringComparison.Ordinal);

                default:
                    try { return existingValue.Equals(targetValue); }
                    catch { return false; }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AddVerboseItem(TreeView treeView, bool skipped, string Key, string valueName, object Value, RegistryValueKind RegType)
        {
            if (treeView == null || Program.Settings?.ThemeLog?.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.Detailed) return;

            if (skipped && !Program.Settings.ThemeLog.ShowSkippedItemsOnDetailedVerbose) return;

            string valueNameLog = string.IsNullOrWhiteSpace(valueName) ? "(default)" : valueName;
            string valueLog = Value switch
            {
                bool b => (b ? 1 : 0).ToString(),
                byte[] v => BitConverter.ToString(v).Replace("-", " "),
                _ => Value?.ToString() ?? "null"
            };

            string details = string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegAdd, Key, valueNameLog, valueLog, RegType.ToString());

            if (skipped)
            {
                details = string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegSkipped, details);
            }

            ThemeLog.AddNode(treeView, details, skipped ? "reg_skip" : "reg_add");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AddVerboseItem_DelValue(TreeView treeView, string Key, string ValueName)
        {
            if (treeView == null || Program.Settings?.ThemeLog?.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.Detailed) return;

            string v0 = string.IsNullOrWhiteSpace(ValueName) ? "(default)" : ValueName;
            ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegDelete, $"{Key}: {v0}"), "reg_delete");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AddVerboseItem_DelKey(TreeView treeView, string Key)
        {
            if (treeView == null || Program.Settings?.ThemeLog?.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.Detailed) return;

            ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegDelete, Key), "reg_delete");
        }

        private static void AddVerboseException(TreeView treeView, Exception ex, string Key, string ValueName, object Value, RegistryValueKind RegType)
        {
            string details;

            if (Program.Settings?.ThemeLog?.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string valueNameLog = string.IsNullOrWhiteSpace(ValueName) ? "(default)" : ValueName;
                string valueLog = Value switch
                {
                    bool b => (b ? 1 : 0).ToString(),
                    byte[] v => BitConverter.ToString(v).Replace("-", " "),
                    _ => Value?.ToString() ?? "null"
                };

                details = $"{ex.Message} - CMD: {string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegAdd, Key, valueNameLog, valueLog, RegType.ToString())}";
                treeView?.InvokeIfNeeded(() => ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {details}", "error"));
                Exceptions.ThemeApply.Add(new Tuple<string, Exception>(details, ex));
            }
            else
            {
                details = ex.Message;
                treeView?.InvokeIfNeeded(() => ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {details}", "error"));
                Exceptions.ThemeApply.Add(new Tuple<string, Exception>(details, ex));
            }

            Program.Log?.WriteReg(LogEventLevel.Error, "Registry exception error", ex);
        }

        public static RegistryValueKind GetRegistryValueKind(object value, bool preferQWord = false)
        {
            if (value == null) return RegistryValueKind.String;

            return value switch
            {
                int or uint => RegistryValueKind.DWord,
                long or ulong => RegistryValueKind.QWord,
                bool => RegistryValueKind.DWord,
                string => RegistryValueKind.String,
                string[] => RegistryValueKind.MultiString,
                byte[] => RegistryValueKind.Binary,
                float f => FloatKind(f, preferQWord),
                double d => FloatKind(d, preferQWord),
                decimal m => FloatKind((double)m, preferQWord),
                _ when value.GetType().IsEnum => HandleEnum(value),
                _ when preferQWord && value is IConvertible conv => HandleConvertible(conv),
                _ => throw new ArgumentException($"Cannot infer RegistryValueKind for type '{value.GetType().FullName}'.", nameof(value))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static RegistryValueKind HandleEnum(object value)
        {
            var underlying = Enum.GetUnderlyingType(value.GetType());
            return underlying == typeof(long) || underlying == typeof(ulong) ? RegistryValueKind.QWord : RegistryValueKind.DWord;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static RegistryValueKind HandleConvertible(IConvertible conv)
        {
            try
            {
                conv.ToInt64(CultureInfo.InvariantCulture);
                return RegistryValueKind.QWord;
            }
            catch
            {
                return RegistryValueKind.DWord;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static RegistryValueKind FloatKind(double number, bool preferQWord)
        {
            if (number >= int.MinValue && number <= int.MaxValue && number == Math.Truncate(number))
                return RegistryValueKind.DWord;

            if (preferQWord && number >= long.MinValue && number <= long.MaxValue && number == Math.Truncate(number))
                return RegistryValueKind.QWord;

            throw new ArgumentOutOfRangeException(nameof(number), number, "Value is outside the range of DWORD/QWORD or not a whole number.");
        }

        public static void WriteReg(TreeView treeView, string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.Unknown)
        {
            WriteReg(Key, ValueName, Value, RegType, treeView);
        }

        public static void WriteReg_CMD(TreeView treeView, string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.Unknown)
        {
            WriteReg_CMD(Key, ValueName, Value, RegType, treeView);
        }

        public static void WriteReg(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.Unknown, TreeView treeView = null)
        {
            if (string.IsNullOrEmpty(Key)) return;

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            string Key_BeforeModification = Key;
            (string processedKey, RegScope scope) = FormatKey(Key_BeforeModification);

            RegistryKey baseKey = null;
            RegistryKey subKey = null;

            try
            {
                baseKey = OpenBaseKey(scope);

                if (RegType == RegistryValueKind.Unknown) RegType = GetRegistryValueKind(Value);

                if (RegType == RegistryValueKind.String && Value == null) Value = string.Empty;

                // Check if we can skip
                object existingValue = ReadRegRaw(Key_BeforeModification, ValueName, null, skipLogging: true);
                if (existingValue != null && CanSkip(existingValue, Value, RegType))
                {
                    if (Program.Settings.ThemeLog.ShowSkippedItemsOnDetailedVerbose)
                    {
                        Program.Log?.WriteRegWrite(LogEventLevel.Information, $"(EditReg skipped) `{Key_BeforeModification}` > `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}`, existing value `{existingValue}` with value type `{RegType}`");
                    }
                    AddVerboseItem(treeView, true, Key_BeforeModification, ValueName, Value, RegType);
                    return;
                }

                Program.Log?.WriteRegWrite(LogEventLevel.Information, $"(EditReg) `{Key_BeforeModification}` > `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}`, new value `{Value}` with value type `{RegType}`");

                // Try direct write first
                if (CanWriteDirect(scope))
                {
                    try
                    {
                        // Ensure key exists
                        subKey = baseKey.OpenSubKey(processedKey, RegistryKeyPermissionCheck.ReadWriteSubTree) ?? baseKey.CreateSubKey(processedKey, true);

                        if (RegType == RegistryValueKind.DWord && Value is bool boolVal)
                        {
                            subKey.SetValue(ValueName, boolVal ? 1 : 0, RegistryValueKind.DWord);
                        }
                        else
                        {
                            subKey.SetValue(ValueName, Value, RegType);
                        }

                        AddVerboseItem(treeView, false, Key_BeforeModification, ValueName, Value, RegType);
                        return;
                    }
                    catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException)
                    {
                        Program.Log?.WriteRegWrite(LogEventLevel.Error, $"Access exception: {ex.Message}");
                        // Fall through to CMD method
                    }
                }

                // Fallback to CMD method
                WriteReg_CMD(treeView, Key_BeforeModification, ValueName, Value, RegType);
            }
            catch (Exception ex)
            {
                Program.Log?.WriteRegWrite(LogEventLevel.Error, $"Registry write exception: {ex.Message}");

                try { WriteReg_CMD(treeView, Key_BeforeModification, ValueName, Value, RegType); }
                catch { AddVerboseException(treeView, ex, processedKey, ValueName, Value, RegType); }
            }
            finally
            {
                subKey?.Close();
                baseKey?.Close();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CanWriteDirect(RegScope scope)
        {
            return Program.Elevated || (scope != RegScope.HKEY_LOCAL_MACHINE && scope != RegScope.HKEY_USERS);
        }

        public static void WriteReg_CMD(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.Unknown, TreeView treeView = null)
        {
            if (string.IsNullOrEmpty(Key)) return;

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            string Key_BeforeModification = Key;
            Key = FormatKey_CMD(Key);

            Program.Log?.WriteRegWrite(LogEventLevel.Information, $"Setting value `{ValueName}` to `{Value}` in `{Key_BeforeModification}` using REG.EXE");

            if (RegType == RegistryValueKind.Unknown) RegType = GetRegistryValueKind(Value);

            string regTemplate;
            string _Value;

            if (Value != null)
            {
                (regTemplate, _Value) = BuildRegCommand(Value, RegType);
            }
            else
            {
                regTemplate = "add \"{0}\" /v \"{1}\" /d \"{2}\" /f";
                _Value = string.Empty;
            }

            if (_Value.Contains('%')) _Value = _Value.Replace("%", "^%");

            try
            {
                Program.SendCommand($"reg {string.Format(regTemplate, Key, ValueName, _Value)}");
            }
            catch (Exception ex)
            {
                Program.Log?.WriteRegWrite(LogEventLevel.Error, $"REG.EXE execution error: {ex.Message}");
                AddVerboseException(treeView, ex, Key_BeforeModification, ValueName, Value, RegType);
            }
            finally
            {
                AddVerboseItem(treeView, false, $"CMD: {Key_BeforeModification}", ValueName, Value, RegType);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (string template, string value) BuildRegCommand(object Value, RegistryValueKind RegType)
        {
            return RegType switch
            {
                RegistryValueKind.String => ("add \"{0}\" /v \"{1}\" /t REG_SZ /d \"{2}\" /f", Value.ToString()),
                RegistryValueKind.DWord => ("add \"{0}\" /v \"{1}\" /t REG_DWORD /d {2} /f", ((int)Value).ToStringDWord()),
                RegistryValueKind.QWord => ("add \"{0}\" /v \"{1}\" /t REG_QWORD /d {2} /f", ((int)Value).ToStringQWord()),
                RegistryValueKind.Binary => ("add \"{0}\" /v \"{1}\" /t REG_BINARY /d {2} /f", BitConverter.ToString((byte[])Value).Replace("-", string.Empty)),
                RegistryValueKind.ExpandString => ("add \"{0}\" /v \"{1}\" /t REG_EXPAND_SZ /d \"{2}\" /f", Value.ToString()),
                RegistryValueKind.MultiString => ("add \"{0}\" /v \"{1}\" /t REG_MULTI_SZ /d \"{2}\" /f", $"{Value.ToString().Replace("\r\n", @"\0")}\\0\\0"),
                RegistryValueKind.None => ("add \"{0}\" /v \"{1}\" /t REG_NONE /d \"{2}\" /f", Value.ToString()),
                _ => ("add \"{0}\" /v \"{1}\" /d \"{2}\" /f", Value.ToString())
            };
        }

        public static T ReadReg<T>(string key, string valueName, T defaultValue = default!, bool raiseExceptions = false, bool ifNullReturnDefaultValue = true)
        {
            object raw = ReadRegRaw(key, valueName, defaultValue!, raiseExceptions, ifNullReturnDefaultValue);

            if (raw == null) return defaultValue;

            try
            {
                if (raw is T tVal) return tVal;

                Type targetType = typeof(T);

                if (targetType.IsEnum)
                {
                    if (raw is IConvertible)
                    {
                        var intVal = Convert.ToInt32(raw, CultureInfo.InvariantCulture);
                        return (T)Enum.ToObject(targetType, intVal);
                    }
                    return (T)Enum.Parse(targetType, raw.ToString()!, true);
                }

                if (targetType == typeof(Color))
                {
                    return (T)(object)ConvertToColor(raw, defaultValue);
                }

                if (targetType == typeof(string[]) && raw is string[] arr)
                {
                    return (T)(object)arr;
                }

                return (T)Convert.ChangeType(raw, targetType, CultureInfo.InvariantCulture);
            }
            catch
            {
                return defaultValue;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Color ConvertToColor(object raw, object defaultValue)
        {
            try
            {
                return raw switch
                {
                    int i => Color.FromArgb(i),
                    long l when l <= int.MaxValue && l >= int.MinValue => Color.FromArgb((int)l),
                    string s => ParseColorString(s),
                    _ => defaultValue is Color c ? c : Color.Empty
                };
            }
            catch
            {
                return defaultValue is Color c ? c : Color.Empty;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Color ParseColorString(string s)
        {
            s = s.Trim();
            if (s.StartsWith("#", StringComparison.Ordinal))
            {
                s = s.Substring(1);
                if (s.Length == 6)
                {
                    return Color.FromArgb(255,
                        Convert.ToInt32(s.Substring(0, 2), 16),
                        Convert.ToInt32(s.Substring(2, 2), 16),
                        Convert.ToInt32(s.Substring(4, 2), 16));
                }
                if (s.Length == 8)
                {
                    return Color.FromArgb(
                        Convert.ToInt32(s.Substring(0, 2), 16),
                        Convert.ToInt32(s.Substring(2, 2), 16),
                        Convert.ToInt32(s.Substring(4, 2), 16),
                        Convert.ToInt32(s.Substring(6, 2), 16));
                }
            }
            return Color.FromName(s);
        }

        private static object ReadRegRaw(string Key, string ValueName, object DefaultValue, bool RaiseExceptions = false, bool IfNullReturnDefaultValue = true, bool skipLogging = false)
        {
            if (string.IsNullOrEmpty(Key)) return DefaultValue;

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            string Key_BeforeModification = Key;
            (string processedKey, RegScope scope) = FormatKey(Key_BeforeModification);

            RegistryKey baseKey = null;
            RegistryKey subKey = null;

            try
            {
                baseKey = OpenBaseKey(scope);

                using (subKey = baseKey.OpenSubKey(processedKey, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey))
                {
                    object result = subKey?.GetValue(ValueName, DefaultValue);

                    if (result?.ToString()?.StartsWith("#USR:", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        result = ReadReg($"HKEY_REAL_CURRENT_USER\\{result.ToString().Replace("#USR:", string.Empty)}", ValueName, DefaultValue, RaiseExceptions, IfNullReturnDefaultValue);
                    }

                    if (!skipLogging)
                        Program.Log?.WriteRegRead(LogEventLevel.Information, $"(GetReg) `{Key_BeforeModification}` > `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}` returned `{(IfNullReturnDefaultValue && result == null ? DefaultValue : result)}`");

                    return IfNullReturnDefaultValue && result == null ? DefaultValue : result;
                }
            }
            catch (Exception ex)
            {
                Program.Log?.WriteRegRead(LogEventLevel.Error, "Registry read error", ex);
                Exceptions.ThemeLoad.Add(new Tuple<string, Exception>($"{Key_BeforeModification} : {ValueName}", ex));

                if (RaiseExceptions) Forms.BugReport.Throw(ex);
                return DefaultValue;
            }
            finally
            {
                subKey?.Close();
                baseKey?.Close();
            }
        }

        public static string[] GetValueNames(string Key)
        {
            if (string.IsNullOrEmpty(Key)) return Array.Empty<string>();

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            string Key_BeforeModification = Key;
            (string processedKey, RegScope scope) = FormatKey(Key_BeforeModification);

            RegistryKey baseKey = null;
            RegistryKey subKey = null;

            try
            {
                baseKey = OpenBaseKey(scope);

                using (subKey = baseKey.OpenSubKey(processedKey, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey))
                {
                    if (subKey == null) return Array.Empty<string>();

                    string[] result = subKey.GetValueNames();
                    Program.Log?.WriteRegRead(LogEventLevel.Information, $"GetValueNames({Key_BeforeModification}) returned `{string.Join(", ", result)}`");
                    return result;
                }
            }
            catch
            {
                return [];
            }
            finally
            {
                subKey?.Close();
                baseKey?.Close();
            }
        }

        public static string[] GetSubKeys(string Key)
        {
            if (string.IsNullOrEmpty(Key)) return Array.Empty<string>();

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            string Key_BeforeModification = Key;
            (string processedKey, RegScope scope) = FormatKey(Key_BeforeModification);

            RegistryKey baseKey = null;
            RegistryKey subKey = null;

            try
            {
                baseKey = OpenBaseKey(scope);

                using (subKey = baseKey.OpenSubKey(processedKey, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadKey))
                {
                    if (subKey == null) return Array.Empty<string>();

                    string[] result = subKey.GetSubKeyNames();
                    Program.Log?.WriteRegRead(LogEventLevel.Information, $"GetSubKeys({Key_BeforeModification}) `{string.Join(", ", result)}`");
                    return result;
                }
            }
            catch
            {
                return [];
            }
            finally
            {
                subKey?.Close();
                baseKey?.Close();
            }
        }

        public static void DeleteKey(string Key, bool deleteSubKeysAndValuesOnly = false, TreeView treeView = null)
        {
            if (string.IsNullOrEmpty(Key)) return;

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            string Key_BeforeModification = Key;
            (string processedKey, RegScope scope) = FormatKey(Key_BeforeModification);

            RegistryKey baseKey = null;

            try
            {
                baseKey = OpenBaseKey(scope);

                Program.Log?.WriteRegDel(LogEventLevel.Information, $"Deleting registry key: {Key_BeforeModification}");

                baseKey.DeleteSubKeyTree(processedKey, true);

                if (deleteSubKeysAndValuesOnly)
                {
                    Program.Log?.WriteRegDel(LogEventLevel.Information, "Keeping the key intact and empty");
                    baseKey.CreateSubKey(processedKey, true);
                }

                AddVerboseItem_DelKey(treeView, Key_BeforeModification);
            }
            catch
            {
                Program.Log?.WriteRegDel(LogEventLevel.Error, "Falling back to REG.EXE for key deletion");
                DeleteKeyAsAdministrator(Key_BeforeModification);
                AddVerboseItem_DelKey(treeView, Key_BeforeModification);
            }
            finally
            {
                baseKey?.Close();
            }
        }

        public static void DeleteKey(TreeView treeView, string Key, bool deleteSubKeysAndValuesOnly = false)
        {
            DeleteKey(Key, deleteSubKeysAndValuesOnly, treeView);
        }

        public static void DeleteValue(string Key, string ValueName, TreeView treeView = null)
        {
            if (string.IsNullOrEmpty(Key)) return;

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            string Key_BeforeModification = Key;
            (string processedKey, RegScope scope) = FormatKey(Key_BeforeModification);

            RegistryKey baseKey = null;
            RegistryKey subKey = null;

            try
            {
                baseKey = OpenBaseKey(scope);

                using (subKey = baseKey.OpenSubKey(processedKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (subKey == null) return;

                    Program.Log?.WriteRegDel(LogEventLevel.Information, $"(Registry DelValue) `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}` from `{Key_BeforeModification}`.");

                    subKey.DeleteValue(ValueName, true);
                    AddVerboseItem_DelValue(treeView, Key_BeforeModification, ValueName);
                }
            }
            catch
            {
                Program.Log?.WriteRegDel(LogEventLevel.Error, "Falling back to REG.EXE for value deletion");
                DeleteValueAsAdministrator(Key_BeforeModification, ValueName);
                AddVerboseItem_DelValue(treeView, Key_BeforeModification, ValueName);
            }
            finally
            {
                subKey?.Close();
                baseKey?.Close();
            }
        }

        public static void DeleteValue(TreeView treeView, string Key, string ValueName)
        {
            DeleteValue(Key, ValueName, treeView);
        }

        public static bool KeyExists(string Key)
        {
            if (string.IsNullOrEmpty(Key)) return false;

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            (string processedKey, RegScope scope) = FormatKey(Key);

            RegistryKey baseKey = null;
            RegistryKey subKey = null;

            try
            {
                baseKey = OpenBaseKey(scope);

                using (subKey = baseKey.OpenSubKey(processedKey, RegistryKeyPermissionCheck.ReadSubTree))
                {
                    return subKey != null;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                subKey?.Close();
                baseKey?.Close();
            }
        }

        public static bool ValueExists(string Key, string ValueName)
        {
            if (string.IsNullOrEmpty(Key)) return false;

            if (Key.StartsWith(ComputerPrefix, StringComparison.OrdinalIgnoreCase)) Key = Key.Substring(ComputerPrefix.Length);

            (string processedKey, RegScope scope) = FormatKey(Key);

            RegistryKey baseKey = null;
            RegistryKey subKey = null;

            try
            {
                baseKey = OpenBaseKey(scope);

                using (subKey = baseKey.OpenSubKey(processedKey, RegistryKeyPermissionCheck.ReadSubTree))
                {
                    if (subKey == null) return false;

                    object value = subKey.GetValue(ValueName, null);
                    return value != null && !string.IsNullOrEmpty(value.ToString());
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                subKey?.Close();
                baseKey?.Close();
            }
        }

        public static void DeleteValueAsAdministrator(string Key, string ValueName)
        {
            if (string.IsNullOrEmpty(Key)) return;

            string cmdKey = FormatKey_CMD(Key);
            Program.Log?.WriteRegDel(LogEventLevel.Information, $"REG.EXE delete: reg delete \"{cmdKey}\\{ValueName}\" /f");
            Program.SendCommand($"reg delete \"{cmdKey}\\{ValueName}\" /f");
        }

        public static void DeleteKeyAsAdministrator(string Key)
        {
            if (string.IsNullOrEmpty(Key)) return;

            string cmdKey = FormatKey_CMD(Key);
            Program.Log?.WriteRegDel(LogEventLevel.Information, $"REG.EXE delete: reg delete \"{cmdKey}\" /f");
            Program.SendCommand($"reg delete \"{cmdKey}\" /f");
        }

        public static void SFC(string File = "", bool IfNotExist_DoScannow = false, bool Hide = true)
        {
            string system32 = SysPaths.System32;
            string cmdPath = $"{system32}\\cmd.exe";

            if (!System.IO.File.Exists(cmdPath)) return;

            IntPtr intPtr = IntPtr.Zero;

            try
            {
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);

                string arguments;
                if (System.IO.File.Exists(File))
                {
                    arguments = $"/c sfc.exe /SCANFILE=\"{File}\"";
                }
                else if (IfNotExist_DoScannow)
                {
                    arguments = "/c sfc.exe /scannow";
                }
                else
                {
                    return;
                }

                if (!Hide) arguments += " && pause";

                Program.Log?.Write(LogEventLevel.Information, $"Starting SFC scan: {arguments}");

                using (Process process = new())
                {
                    process.StartInfo = new()
                    {
                        FileName = cmdPath,
                        Arguments = arguments,
                        Verb = "runas",
                        WindowStyle = Hide ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal,
                        CreateNoWindow = Hide,
                        UseShellExecute = true
                    };

                    process.Start();
                    process.WaitForExit();
                }

                Program.Log?.Write(LogEventLevel.Information, "SFC scan finished");
            }
            finally
            {
                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
            }
        }

        /// <summary>
        /// Take ownership of a File for the current Windows user
        /// </summary>
        /// <param name="filepath"></param>
        public static void TakeOwnership(string filepath, bool AsAdministrator = false)
        {
            if (!System.IO.File.Exists(filepath))
            {
                Program.Log?.Write(LogEventLevel.Error, $"File does not exist: {filepath}");
                return;
            }

            TakeOwnership_CMD(filepath, AsAdministrator);
            TakeOwnership_ICACLS(filepath, AsAdministrator);
            TakeOwnership_DotNet(filepath, AsAdministrator);
        }

        private static void TakeOwnership_CMD(string File, bool AsAdministrator = false)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Taking ownership of file: {File}");

            string cmd = $"{SysPaths.TakeOwn} /f \"{File}\"{(AsAdministrator ? " /a" : string.Empty)}";
            Program.Log?.Write(LogEventLevel.Information, $"Command: {cmd}");

            Program.SendCommand(cmd);
        }

        private static bool TakeOwnership_DotNet(string File, bool AsAdministrator = false)
        {
            try
            {
                Program.Log?.Write(LogEventLevel.Information, $"Setting .NET access control for: {File}");

                var fSecurity = System.IO.File.GetAccessControl(File);
                fSecurity.AddAccessRule(new FileSystemAccessRule(User.Identity.Name, FileSystemRights.FullControl, AccessControlType.Allow));
                System.IO.File.SetAccessControl(File, fSecurity);
                return true;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Failed to set .NET access control", ex);
                Forms.BugReport.Throw(ex);
                return false;
            }
        }

        private static void TakeOwnership_ICACLS(string File, bool AsAdministrator = false)
        {
            string user = AsAdministrator ? "administrators" : "%username%";
            string cmd = $"{SysPaths.System32}\\ICACLS.exe \"{File}\" /grant {user}:F";

            Program.Log?.Write(LogEventLevel.Information, $"ICACLS: {cmd}");
            Program.SendCommand(cmd);
        }

        public static void Move_File(string source, string destination)
        {
            if (!System.IO.File.Exists(source))
            {
                Program.Log?.Write(LogEventLevel.Error, $"Source file does not exist: {source}");
                return;
            }

            Program.Log?.Write(LogEventLevel.Information, $"Moving file `{source}` to `{destination}`");
            Program.Log?.Write(LogEventLevel.Information, "Using command prompt for file move");

            Program.SendCommand($"{SysPaths.CMD} /C move \"{source}\" \"{destination}\" && exit");
        }
    }
}