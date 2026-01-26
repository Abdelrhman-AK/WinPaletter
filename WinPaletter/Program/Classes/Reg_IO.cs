using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.AccessControl;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// Provides utility methods for interacting with the Windows Registry, including reading, writing,  deleting, and
    /// managing registry keys and values. This class also includes methods for logging  registry operations and
    /// handling security or access issues.
    /// </summary>
    /// <remarks>The <see cref="Reg_IO"/> class is designed to simplify common registry operations while
    /// providing  detailed logging and error handling. It supports operations such as creating, editing, and deleting 
    /// registry keys and values, as well as checking for their existence. The class also includes methods  for handling
    /// elevated permissions and alternative approaches (e.g., using command-line tools) to  address access
    /// restrictions.  This class is particularly useful in scenarios where registry modifications are required as part
    /// of  application configuration or system management tasks. It includes support for verbose logging to  assist
    /// with debugging and auditing registry changes.  Note: Many methods in this class require appropriate permissions
    /// to access or modify the registry.  Ensure that the application is running with sufficient privileges when
    /// performing operations on  protected registry keys.</remarks>
    public class Reg_IO
    {
        /// <summary>
        /// Specifies the registry view to be used based on the operating system's architecture.
        /// </summary>
        /// <remarks>If the operating system is 64-bit, the registry view is set to <see
        /// cref="RegistryView.Registry64"/>. Otherwise, it defaults to <see cref="RegistryView.Default"/>.</remarks>
        private static readonly RegistryView regView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default;

        /// <summary>
        /// Specifies the scope of a Windows Registry key.
        /// </summary>
        /// <remarks>This enumeration defines the root keys in the Windows Registry that can be used to access  specific
        /// sections of the registry. Each value corresponds to a predefined root key.</remarks>
        private enum RegScope
        {
            HKEY_CURRENT_USER,
            HKEY_LOCAL_MACHINE,
            HKEY_USERS,
            HKEY_CLASSES_ROOT,
            HKEY_CURRENT_CONFIG
        }

        /// <summary>
        /// Processes a registry key string and determines its associated registry scope.
        /// </summary>
        /// <remarks>If the <paramref name="Key"/> string starts with "Computer\", this prefix will be removed before
        /// further processing. The method identifies the registry hive based on the prefix of the key string (e.g.,
        /// "HKEY_CURRENT_USER", "HKEY_LOCAL_MACHINE"). If no recognized hive is found, the default scope remains
        /// unchanged.</remarks>
        /// <param name="Key">The registry key string to process. This can include a full or partial path starting with a registry hive (e.g.,
        /// "HKEY_CURRENT_USER\Software\Example") or a path prefixed with "Computer\".</param>
        /// <param name="scope">The initial registry scope to use. Defaults to <see cref="RegScope.HKEY_CURRENT_USER"/>.  This value will be updated
        /// to reflect the registry hive identified in the <paramref name="Key"/> string.</param>
        /// <returns>A tuple containing the processed registry key string and the determined <see cref="RegScope"/> value. The key string
        /// will have the registry hive prefix removed, and the scope will indicate the corresponding registry hive.</returns>
        private static (string, RegScope) FormatKey(string Key, RegScope scope = RegScope.HKEY_CURRENT_USER)
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
        /// Converts a registry key path from its full form to a shorthand notation commonly used in command-line tools.
        /// </summary>
        /// <remarks>The method handles several common registry key prefixes, including: <list type="bullet">
        /// <item><description><c>HKEY_LOCAL_MACHINE</c> is converted to <c>HKLM</c>.</description></item>
        /// <item><description><c>HKEY_CURRENT_USER</c> is converted to <c>HKCU</c> or <c>HKU\{User.SID}</c> depending on the
        /// user's context.</description></item> <item><description><c>HKEY_USERS</c> is converted to
        /// <c>HKU</c>.</description></item> <item><description><c>HKEY_CLASSES_ROOT</c> is converted to
        /// <c>HKCR</c>.</description></item> <item><description><c>HKEY_CURRENT_CONFIG</c> is converted to
        /// <c>HKCC</c>.</description></item> </list> If the input key starts with "Computer\", this prefix is removed before
        /// further processing.</remarks>
        /// <param name="Key">The full registry key path to process. This value is case-insensitive and may include prefixes such as
        /// "HKEY_LOCAL_MACHINE" or "Computer\".</param>
        /// <returns>A string containing the processed registry key path in shorthand notation. For example, "HKEY_LOCAL_MACHINE" is
        /// converted to "HKLM".</returns>
        private static string FormatKey_CMD(string Key)
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
        /// Opens a base registry key corresponding to the specified scope.
        /// </summary>
        /// <remarks>The method maps the provided <paramref name="scope"/> to the appropriate registry hive and opens it 
        /// using the current registry view. For <see cref="RegScope.HKEY_CURRENT_USER"/>, the method attempts to  open the
        /// registry key for the current user's SID if applicable; otherwise, it defaults to the  <see
        /// cref="RegistryHive.CurrentUser"/> hive. If an error occurs while resolving the user's SID, the  method falls back to
        /// opening the <see cref="RegistryHive.CurrentUser"/> hive.</remarks>
        /// <param name="scope">The registry scope to open. This determines which base registry hive is accessed, such as  <see
        /// cref="RegScope.HKEY_CURRENT_USER"/> or <see cref="RegScope.HKEY_LOCAL_MACHINE"/>.</param>
        /// <returns>A <see cref="RegistryKey"/> object representing the opened base registry key for the specified scope.</returns>
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
        /// Determines whether the specified registry value can be skipped from being updated based on its current value and the
        /// target value.
        /// </summary>
        /// <remarks>This method compares the current and target values for various registry value types, including 
        /// strings, binary data, and numeric types, to determine if an update is necessary. If the values  are equivalent, the
        /// method avoids unnecessary registry writes, improving performance.</remarks>
        /// <param name="existingValue">The current value of the registry key.</param>
        /// <param name="targetValue">The target value to be set for the registry key.</param>
        /// <param name="RegType">The type of the registry value. If <see cref="RegistryValueKind.Unknown"/> is provided,  the type will be inferred
        /// from <paramref name="targetValue"/>.</param>
        /// <returns><see langword="true"/> if the current value matches the target value and the update can be skipped;  otherwise, <see
        /// langword="false"/>.</returns>
        private static bool CanSkip(object existingValue, object targetValue, RegistryValueKind RegType = RegistryValueKind.Unknown)
        {
            bool skip = existingValue is null && targetValue is null;

            // Trying to convert and compare the values to avoid unnecessary registry writes and save time.
            if (existingValue is not null)
            {
                RegType = RegType == RegistryValueKind.Unknown ? GetRegistryValueKind(targetValue) : RegType;

                switch (RegType)
                {
                    case RegistryValueKind.MultiString:
                        {
                            var a = (string[])existingValue;
                            var b = (string[])targetValue;
                            return a.Length == b.Length && a.Zip(b, (x, y) => string.Equals(x, y, StringComparison.Ordinal)).All(eq => eq);
                        }

                    case RegistryValueKind.Binary:
                        {
                            return existingValue is byte[] eb && targetValue is byte[] tb && eb.Length == tb.Length && eb.AsSpan().SequenceEqual(tb);
                        }

                    case RegistryValueKind.DWord: // int
                        {
                            if (targetValue is not bool)
                            {
                                try
                                {
                                    int conversion_0 = (int)existingValue;
                                    int conversion_1 = (int)targetValue;
                                    skip = conversion_0.Equals(conversion_1);
                                }
                                catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.
                            }
                            else
                            {
                                try
                                {
                                    int conversion_0 = (int)existingValue;
                                    int conversion_1 = (bool)targetValue ? 1 : 0;
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
                                    ulong conversion_0 = (ulong)existingValue;
                                    ulong conversion_1 = (ulong)targetValue;
                                    skip = conversion_0.Equals(conversion_1);
                                }
                                catch { } // Conversion and comparison failed. Anyway, we won't skip setting registry.
                            }
                            else
                            {
                                try
                                {
                                    ulong conversion_0 = (ulong)existingValue;
                                    ulong conversion_1 = (bool)targetValue ? 1u : 0u;
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
        /// Adds a detailed log entry to the specified <see cref="TreeView"/> control, representing a registry operation.
        /// </summary>
        /// <remarks>This method logs detailed information about a registry operation, including the key, value name,
        /// value, and type, if the verbose logging level is set to <see
        /// cref="Settings.Structures.ThemeLog.VerboseLevels.Detailed"/>. If the operation is skipped and <see
        /// cref="Program.Settings.ThemeLog.ShowSkippedItemsOnDetailedVerbose"/> is <see langword="false"/>, no log entry is
        /// added.</remarks>
        /// <param name="treeView">The <see cref="TreeView"/> control to which the log entry will be added. If null, the method does nothing.</param>
        /// <param name="skipped">A value indicating whether the registry operation was skipped. If <see langword="true"/>, the log entry will
        /// indicate a skipped operation.</param>
        /// <param name="Key">The registry key associated with the operation.</param>
        /// <param name="valueName">The name of the registry value being logged. If empty or null, it is treated as the default value.</param>
        /// <param name="Value">The value associated with the registry operation. This can be a <see cref="bool"/>, a <see cref="byte"/> array, or
        /// another object.</param>
        /// <param name="RegType">The type of the registry value, represented as a <see cref="RegistryValueKind"/>.</param>
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
                    valueLog = ((bool)Value ? 1 : 0).ToString();
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
                    details = string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegAdd, Key, valueNameLog, valueLog, RegType.ToString());
                    imageKey = "reg_add";
                }
                else
                {
                    if (!Program.Settings.ThemeLog.ShowSkippedItemsOnDetailedVerbose)
                        return;
                    details = string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegSkipped, string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegAdd, Key, valueNameLog, valueLog, RegType.ToString()));
                    imageKey = "reg_skip";
                }
                ThemeLog.AddNode(treeView, details, imageKey);
            }
        }

        /// <summary>
        /// Adds a detailed log entry to the specified <see cref="TreeView"/> when a registry value is deleted, if the
        /// verbose logging level is set to detailed.
        /// </summary>
        /// <remarks>This method only adds a log entry if the verbose logging level is set to <see
        /// cref="Settings.Structures.ThemeLog.VerboseLevels.Detailed"/>. The log entry includes the registry key and
        /// value name, formatted for display.</remarks>
        /// <param name="treeView">The <see cref="TreeView"/> control to which the log entry will be added. If null, no action is taken.</param>
        /// <param name="Key">The registry key associated with the deleted value.</param>
        /// <param name="ValueName">The name of the registry value being deleted. If null or whitespace, it is treated as the default value.</param>
        private static void AddVerboseItem_DelValue(TreeView treeView, string Key, string ValueName)
        {
            if (treeView is null)
                return;

            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string v0 = ValueName;

                if (string.IsNullOrWhiteSpace(v0))
                    v0 = "(default)";

                ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegDelete, $"{Key}: {v0}"), "reg_delete");
            }
        }

        /// <summary>
        /// Adds a verbose log entry to the specified <see cref="TreeView"/> when a registry key is deleted.
        /// </summary>
        /// <remarks>A log entry is added only if the verbose level is set to <see
        /// cref="Settings.Structures.ThemeLog.VerboseLevels.Detailed"/>. The log entry includes the key name and is categorized
        /// as "reg_delete".</remarks>
        /// <param name="treeView">The <see cref="TreeView"/> control to which the log entry will be added. If null, no action is taken.</param>
        /// <param name="Key">The name of the registry key being deleted. This value is included in the log entry.</param>
        private static void AddVerboseItem_DelKey(TreeView treeView, string Key)
        {
            if (treeView is null)
                return;

            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegDelete, Key), "reg_delete");
            }
        }

        /// <summary>
        /// Logs detailed information about a registry-related exception and updates the provided <see cref="TreeView"/> with
        /// the error details.
        /// </summary>
        /// <remarks>This method logs detailed error information if the verbose logging level is set to <see
        /// cref="Settings.Structures.ThemeLog.VerboseLevels.Detailed"/>. Otherwise, it logs a simplified error message. The
        /// error details are also added to the global exception list for theme application errors.</remarks>
        /// <param name="treeView">The <see cref="TreeView"/> control to which the error details will be added. Can be <see langword="null"/> if no UI
        /// update is required.</param>
        /// <param name="ex">The <see cref="Exception"/> instance representing the error to be logged.</param>
        /// <param name="Key">The registry key associated with the operation that caused the exception.</param>
        /// <param name="ValueName">The name of the registry value associated with the operation. Can be <see langword="null"/> or empty to indicate the
        /// default value.</param>
        /// <param name="Value">The value being written to the registry when the exception occurred. Can be of any type supported by the registry.</param>
        /// <param name="RegType">The <see cref="RegistryValueKind"/> indicating the type of the registry value being written.</param>
        private static void AddVerboseException(TreeView treeView, Exception ex, string Key, string ValueName, object Value, RegistryValueKind RegType)
        {
            if (Program.Settings.ThemeLog.VerboseLevel == Settings.Structures.ThemeLog.VerboseLevels.Detailed)
            {
                string valueNameLog = ValueName;
                string valueLog;
                if (Value is bool)
                {
                    valueLog = ((bool)Value ? 1 : 0).ToString();
                }
                else if (Value is byte[] v)
                {
                    valueLog = string.Join(" ", v);
                }
                else
                {
                    valueLog = Value.ToString();
                }

                if (string.IsNullOrWhiteSpace(valueNameLog)) valueNameLog = "(default)";
                if (string.IsNullOrWhiteSpace(valueLog)) valueLog = "null";

                string details = $"{ex.Message} - CMD: {string.Format(Program.Localization.Strings.ThemeManager.Advanced.RegAdd, Key, valueNameLog, valueLog, RegType.ToString())}";
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

            Program.Log?.WriteReg(LogEventLevel.Error, $"Registry exception error", ex);
        }

        /// <summary>
        /// Infers the most appropriate RegistryValueKind for a given .NET object.
        /// </summary>
        /// <param name="value">The managed object that will be written to the registry.</param>
        /// <param name="preferQWord">
        /// If true, integers larger than 32-bit default to QWord (REG_QWORD); 
        /// otherwise Int32 is treated as DWord (REG_DWORD).
        /// </param>
        /// <returns>The corresponding RegistryValueKind.</returns>
        /// <exception cref="ArgumentException">Thrown when the type is not supported.</exception>
        public static RegistryValueKind GetRegistryValueKind(object value, bool preferQWord = false)
        {
            if (value is null)
                return RegistryValueKind.String; // null => treat as empty string

            switch (value)
            {
                case int:
                case uint:
                    return RegistryValueKind.DWord;

                case long:
                case ulong:
                    return RegistryValueKind.QWord;

                case bool:
                    return RegistryValueKind.DWord;

                case float f:
                    return FloatKind(f, preferQWord);

                case double d:
                    return FloatKind(d, preferQWord);

                case decimal m:
                    return FloatKind((double)m, preferQWord);

                case string:
                    return RegistryValueKind.String;

                case string[]:
                    return RegistryValueKind.MultiString;

                case byte[]:
                    return RegistryValueKind.Binary;

                default:
                    // Handle any enum by its underlying integral type
                    var type = value.GetType();
                    if (type.IsEnum)
                    {
                        // Use the enum’s underlying type to decide DWord vs QWord
                        var underlying = Enum.GetUnderlyingType(type);
                        if (underlying == typeof(long) || underlying == typeof(ulong))
                            return RegistryValueKind.QWord;

                        return RegistryValueKind.DWord;
                    }

                    // Optional generic numeric coercion
                    if (preferQWord && value is IConvertible conv)
                    {
                        try
                        {
                            long _ = conv.ToInt64(CultureInfo.InvariantCulture);
                            return RegistryValueKind.QWord;
                        }
                        catch { }
                    }

                    throw new ArgumentException(
                        $"Cannot infer RegistryValueKind for type '{type.FullName}'.", nameof(value));
            }
        }

        /// <summary>
        /// Determines the appropriate registry value kind (DWORD or QWORD) for a given floating-point number.
        /// </summary>
        /// <param name="number">The floating-point number to evaluate. Must be a whole number within the range of valid DWORD or QWORD
        /// values.</param>
        /// <param name="preferQWord">A boolean value indicating whether to prefer QWORD if the number is within the range of both DWORD and
        /// QWORD.</param>
        /// <returns><see cref="RegistryValueKind.DWord"/> if the number is within the range of a DWORD;  <see
        /// cref="RegistryValueKind.QWord"/> if <paramref name="preferQWord"/> is <see langword="true"/> and the number
        /// is within the range of a QWORD.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="number"/> is not a whole number.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="number"/> is outside the range of both DWORD and QWORD.</exception>
        private static RegistryValueKind FloatKind(double number, bool preferQWord)
        {
            // Check for whole numbers only, because registry DWORD/QWORD are integers.
            if (number >= int.MinValue && number <= int.MaxValue)
                return RegistryValueKind.DWord;

            if (preferQWord && number >= long.MinValue && number <= long.MaxValue)
                return RegistryValueKind.QWord;

            throw new ArgumentOutOfRangeException(
                nameof(number),
                number,
                "Value is outside the range of DWORD/QWORD.");
        }

        /// <summary>
        /// Modifies or creates a registry value in the Windows Registry.
        /// </summary>
        /// <remarks>This method ensures that the specified registry key exists before attempting to set the value. If the
        /// key does not exist, it will be created.  If the program is running with elevated privileges, the method will attempt
        /// to write directly to the registry. If access is denied or insufficient permissions are detected, the method will
        /// attempt to use an alternative approach to modify the registry value.  The method logs detailed information about the
        /// operation if logging is enabled in the application settings. It also skips writing to the registry if the existing
        /// value matches the new value.</remarks>
        /// <param name="Key">The full path of the registry key to modify. The path may include the "Computer\" prefix, which will be removed
        /// automatically.</param>
        /// <param name="ValueName">The name of the registry value to modify. Use <see langword="null"/> or an empty string to modify the default value
        /// of the key.</param>
        /// <param name="Value">The value to set for the specified registry value. If <paramref name="Value"/> is <see langword="null"/> and the
        /// value type is <see cref="RegistryValueKind.String"/>, it will be replaced with an empty string.</param>
        /// <param name="RegType">The type of the registry value. If <see cref="RegistryValueKind.Unknown"/> is specified, the type will be inferred
        /// from the <paramref name="Value"/>.</param>
        /// <param name="treeView">An optional <see cref="TreeView"/> control to log verbose information about the operation. Can be <see
        /// langword="null"/>.</param>
        public static void WriteReg(TreeView treeView, string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.Unknown)
        {
            WriteReg(Key, ValueName, Value, RegType, treeView);
        }

        /// <summary>
        /// Updates a registry value with the specified key, value name, and data, and optionally updates the associated
        /// TreeView control.
        /// </summary>
        /// <remarks>This method modifies a registry value and optionally updates the provided <see
        /// cref="TreeView"/> control to reflect the changes. If <paramref name="treeView"/> is <see langword="null"/>,
        /// no UI updates will be performed.</remarks>
        /// <param name="treeView">The <see cref="TreeView"/> control to update after modifying the registry. Can be <see langword="null"/> if
        /// no UI update is required.</param>
        /// <param name="Key">The registry key path where the value resides. Cannot be <see langword="null"/> or empty.</param>
        /// <param name="ValueName">The name of the registry value to modify. Cannot be <see langword="null"/> or empty.</param>
        /// <param name="Value">The new data to set for the registry value. Must match the type specified by <paramref name="RegType"/>.</param>
        /// <param name="RegType">The type of the registry value. Defaults to <see cref="RegistryValueKind.Unknown"/> if not specified.</param>
        public static void WriteReg_CMD(TreeView treeView, string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.Unknown)
        {
            WriteReg_CMD(Key, ValueName, Value, RegType, treeView);
        }

        /// <summary>
        /// Modifies or creates a registry value in the Windows Registry.
        /// </summary>
        /// <remarks>This method ensures that the specified registry key exists before attempting to set the value. If the
        /// key does not exist, it will be created.  If the program is running with elevated privileges, the method will attempt
        /// to write directly to the registry. If access is denied or insufficient permissions are detected, the method will
        /// attempt to use an alternative approach to modify the registry value.  The method logs detailed information about the
        /// operation if logging is enabled in the application settings. It also skips writing to the registry if the existing
        /// value matches the new value.</remarks>
        /// <param name="Key">The full path of the registry key to modify. The path may include the "Computer\" prefix, which will be removed
        /// automatically.</param>
        /// <param name="ValueName">The name of the registry value to modify. Use <see langword="null"/> or an empty string to modify the default value
        /// of the key.</param>
        /// <param name="Value">The value to set for the specified registry value. If <paramref name="Value"/> is <see langword="null"/> and the
        /// value type is <see cref="RegistryValueKind.String"/>, it will be replaced with an empty string.</param>
        /// <param name="RegType">The type of the registry value. If <see cref="RegistryValueKind.Unknown"/> is specified, the type will be inferred
        /// from the <paramref name="Value"/>.</param>
        /// <param name="treeView">An optional <see cref="TreeView"/> control to log verbose information about the operation. Can be <see
        /// langword="null"/>.</param>
        public static void WriteReg(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.Unknown, TreeView treeView = null)
        {
            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Process the key to get the scope and key without the scope.
            (string, RegScope) item = FormatKey(Key_BeforeModification);

            // Key without the scope.
            Key = item.Item1;

            // Scope of the key.
            RegScope scope = item.Item2;

            // Open the processed key
            RegistryKey R = OpenBaseKey(scope);

            RegType = RegType == RegistryValueKind.Unknown ? GetRegistryValueKind(Value) : RegType;

            // If the value is null, set it to string.Empty to avoid errors.
            if (RegType == RegistryValueKind.String & Value is null) Value = string.Empty;

            // Create the key if it does not exist.
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
            object existingValue = ReadRegRaw(Key_BeforeModification, ValueName, null);
            if (existingValue is not null && CanSkip(existingValue, Value, RegType))
            {
                Program.Log?.WriteRegWrite(LogEventLevel.Information, $"(EditReg skipped) `{Key_BeforeModification}` > `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}`, existing value `{existingValue}` with value type `{RegType}`");
                AddVerboseItem(treeView, true, Key_BeforeModification, ValueName, Value, RegType);
                return;
            }

            Program.Log?.WriteRegWrite(LogEventLevel.Information, $"(EditReg) `{Key_BeforeModification}` > `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}`, new value `{Value}` with value type `{RegType}`");

            // Set the value to the registry
            try
            {
                // If the program is running as an administrator and the scope is not HKEY_LOCAL_MACHINE or HKEY_USERS, set the value directly.
                // If the program is not running as an administrator and the scope is HKEY_LOCAL_MACHINE or HKEY_USERS, use the EditReg_CMD function to try to solve security access issues.
                if (Program.Elevated && (scope == RegScope.HKEY_LOCAL_MACHINE || scope == RegScope.HKEY_USERS) || !(scope == RegScope.HKEY_LOCAL_MACHINE) & !(scope == RegScope.HKEY_USERS))
                {
                    using (RegistryKey subKey = R.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        if (RegType == RegistryValueKind.DWord && bool.TryParse(Value.ToString(), out bool boolVal))
                        {
                            subKey.SetValue(ValueName, boolVal ? 1 : 0, RegistryValueKind.DWord);
                        }
                        else
                        {
                            subKey.SetValue(ValueName, Value, RegType);
                        }
                    }
                    AddVerboseItem(treeView, false, Key_BeforeModification, ValueName, Value, RegType);
                }
                else if (scope == RegScope.HKEY_LOCAL_MACHINE) { WriteReg_CMD(treeView, $@"HKEY_LOCAL_MACHINE\{Key}", ValueName, Value, RegType); }

                else if (scope == RegScope.HKEY_USERS) { WriteReg_CMD(treeView, $@"HKEY_USERS\{Key}", ValueName, Value, RegType); }
            }
            catch (SecurityException @PermissionEx)
            {
                // A security exception occurred while trying to set the value directly. Try to use the EditReg_CMD function to solve the security access issues.

                Program.Log?.WriteRegWrite(LogEventLevel.Error, $"Security exception: {@PermissionEx.Message}");

                try { WriteReg_CMD(treeView, Key_BeforeModification, ValueName, Value, RegType); }
                catch { AddVerboseException(treeView, @PermissionEx, Key, ValueName, Value, RegType); }
            }
            catch (UnauthorizedAccessException @UnauthorizedAccessEx)
            {
                Program.Log?.WriteRegWrite(LogEventLevel.Error, $"Unauthorized access exception: {@UnauthorizedAccessEx.Message}");

                // An unauthorized access exception occurred while trying to set the value directly. Try to use the EditReg_CMD function to solve the security access issues.
                try { WriteReg_CMD(treeView, Key_BeforeModification, ValueName, Value, RegType); }
                catch { AddVerboseException(treeView, @UnauthorizedAccessEx, Key, ValueName, Value, RegType); }
            }
            catch (Exception ex)
            {
                Program.Log?.WriteRegWrite(LogEventLevel.Error, $"Registry write exception error: {ex.Message}");

                // An exception occurred while trying to set the value directly. Try to use the EditReg_CMD function to solve the security access issues.
                try { WriteReg_CMD(treeView, Key_BeforeModification, ValueName, Value, RegType); }
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
        /// Updates a registry value with the specified key, value name, and data, and optionally updates the associated
        /// TreeView control.
        /// </summary>
        /// <remarks>This method modifies a registry value and optionally updates the provided <see
        /// cref="TreeView"/> control to reflect the changes. If <paramref name="treeView"/> is <see langword="null"/>,
        /// no UI updates will be performed.</remarks>
        /// <param name="treeView">The <see cref="TreeView"/> control to update after modifying the registry. Can be <see langword="null"/> if
        /// no UI update is required.</param>
        /// <param name="Key">The registry key path where the value resides. Cannot be <see langword="null"/> or empty.</param>
        /// <param name="ValueName">The name of the registry value to modify. Cannot be <see langword="null"/> or empty.</param>
        /// <param name="Value">The new data to set for the registry value. Must match the type specified by <paramref name="RegType"/>.</param>
        /// <param name="RegType">The type of the registry value. Defaults to <see cref="RegistryValueKind.Unknown"/> if not specified.</param>
        public static void WriteReg_CMD(string Key, string ValueName, object Value, RegistryValueKind RegType = RegistryValueKind.Unknown, TreeView treeView = null)
        {
            string regTemplate;

            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            string Key_BeforeModification = Key;

            // Process the key to be valid for Command Prompt.
            Key = FormatKey_CMD(Key);

            Program.Log?.WriteRegWrite(LogEventLevel.Information, $"Setting value `{ValueName}` to `{Value}` in `{Key_BeforeModification}` by using REG.EXE instead of native .NET Framework methods.");

            string _Value;

            // /v = Value Name
            // /t = Registry Value Type
            // /d = Value
            // /f = Disable prompt
            if (Value is not null)
            {
                RegType = RegType == RegistryValueKind.Unknown ? GetRegistryValueKind(Value) : RegType;

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
                            _Value = ((int)Value).ToStringDWord();
                            break;
                        }

                    case RegistryValueKind.QWord:
                        {
                            regTemplate = "add \"{0}\" /v \"{1}\" /t REG_QWORD /d {2} /f";
                            _Value = ((int)Value).ToStringQWord();
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
                Program.SendCommand($"reg {string.Format(regTemplate, Key, ValueName, _Value)}");
            }
            catch (Exception ex)
            {
                Program.Log?.WriteRegWrite(LogEventLevel.Error, $"Executing command exception error: {ex.Message}");
                Program.Log?.WriteRegWrite(LogEventLevel.Error, $"Registry edit couldn't be done by the two methods; .NET Framework methods and REG.EXE");

                AddVerboseException(treeView, ex, Key, ValueName, Value, RegType);
            }
            finally
            {
                AddVerboseItem(treeView, false, $"CMD: {Key_BeforeModification}", ValueName, Value, RegType);
            }
        }

        /// <summary>
        /// Reads a value from the Windows Registry and attempts to convert it to the specified type.
        /// </summary>
        /// <remarks>This method supports conversion to common types, including enums, colors, and arrays of strings.  For
        /// enums, both numeric values and string names are supported. For colors, the value is converted using a custom
        /// conversion logic. If <paramref name="raiseExceptions"/> is <see langword="false"/>, any errors during the read or
        /// conversion process will result in the <paramref name="defaultValue"/> being returned.</remarks>
        /// <typeparam name="T">The type to which the registry value should be converted.</typeparam>
        /// <param name="key">The registry key path from which to read the value. This must be a valid registry key path.</param>
        /// <param name="valueName">The name of the registry value to read. If the value does not exist, the <paramref name="defaultValue"/> will be
        /// returned.</param>
        /// <param name="defaultValue">The default value to return if the registry value is not found or cannot be converted to the specified type.</param>
        /// <param name="raiseExceptions">A boolean value indicating whether exceptions should be raised if an error occurs during the read operation.  If
        /// <see langword="true"/>, exceptions will be thrown; otherwise, errors will be silently handled, and the <paramref
        /// name="defaultValue"/> will be returned.</param>
        /// <param name="ifNullReturnDefaultValue">A boolean value indicating whether the <paramref name="defaultValue"/> should be returned if the registry value is
        /// <see langword="null"/>. If <see langword="true"/>, <paramref name="defaultValue"/> will be returned for <see
        /// langword="null"/> values; otherwise, <see langword="null"/> will be returned.</param>
        /// <returns>The value read from the registry, converted to the specified type <typeparamref name="T"/>.  If the value does not
        /// exist, cannot be converted, or an error occurs, the <paramref name="defaultValue"/> is returned.</returns>
        public static T ReadReg<T>(string key, string valueName, T defaultValue = default!, bool raiseExceptions = false, bool ifNullReturnDefaultValue = true)
        {
            object raw = ReadRegRaw(key, valueName, defaultValue!, raiseExceptions, ifNullReturnDefaultValue);

            if (raw is null) return defaultValue;

            try
            {
                // Direct cast if already correct type
                if (raw is T tVal) return tVal;

                Type targetType = typeof(T);

                // Enums: expect numeric value in registry (DWORD/Int32)
                if (targetType.IsEnum)
                {
                    // Convert any numeric to int first
                    if (raw is IConvertible)
                    {
                        var intVal = Convert.ToInt32(raw, CultureInfo.InvariantCulture);
                        return (T)Enum.ToObject(targetType, intVal);
                    }
                    // Fallback if string was stored
                    return (T)Enum.Parse(targetType, raw.ToString()!, true);
                }

                // Color support
                if (targetType == typeof(Color))
                {
                    return (T)(object)ConvertToColor(raw, defaultValue);
                }

                // Array of strings (MultiString registry type)
                if (targetType == typeof(string[]) && raw is IEnumerable<string> enumerable)
                {
                    return (T)(object)enumerable.ToArray();
                }

                // Attempt common type conversion
                return (T)Convert.ChangeType(raw, targetType, CultureInfo.InvariantCulture);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Converts the specified object to a <see cref="System.Drawing.Color"/> instance.
        /// </summary>
        /// <remarks>This method attempts to convert the input object to a color using the following
        /// rules: <list type="number"> <item><description>If the input is an <see cref="int"/>, it is treated as an
        /// ARGB value.</description></item> <item><description>If the input is a <see cref="long"/> within the range of
        /// a 32-bit integer, it is treated as an ARGB value.</description></item> <item><description>If the input is a
        /// <see cref="string"/>, it is interpreted as either a hexadecimal color code (e.g., "#RRGGBB" or "#AARRGGBB")
        /// or a known color name.</description></item> </list> If the input cannot be converted, the method falls back
        /// to the <paramref name="defaultValue"/> if it is a valid <see cref="System.Drawing.Color"/>.  If no valid
        /// fallback is provided, <see cref="System.Drawing.Color.Empty"/> is returned.</remarks>
        /// <param name="raw">The input object to convert. Supported types include: <list type="bullet"> <item><description><see
        /// cref="int"/>: Interpreted as an ARGB value.</description></item> <item><description><see cref="long"/>:
        /// Interpreted as an ARGB value if within the range of a 32-bit integer.</description></item>
        /// <item><description><see cref="string"/>: Interpreted as a hexadecimal color code (e.g., "#RRGGBB" or
        /// "#AARRGGBB") or a known color name.</description></item> </list></param>
        /// <param name="defaultValue">The fallback value to return if the conversion fails. Must be a <see cref="System.Drawing.Color"/> instance.</param>
        /// <returns>A <see cref="System.Drawing.Color"/> instance representing the converted color.  If the conversion fails and
        /// <paramref name="defaultValue"/> is a valid <see cref="System.Drawing.Color"/>,  the default value is
        /// returned. Otherwise, <see cref="System.Drawing.Color.Empty"/> is returned.</returns>
        private static Color ConvertToColor(object raw, object defaultValue)
        {
            try
            {
                switch (raw)
                {
                    case int i:
                        return Color.FromArgb(i);
                    case long l when l <= int.MaxValue && l >= int.MinValue:
                        return Color.FromArgb((int)l);
                    case string s:
                        s = s.Trim();
                        // Hex with or without alpha (#RRGGBB or #AARRGGBB)
                        if (s.StartsWith("#", StringComparison.Ordinal))
                        {
                            s = s.Substring(1);
                            if (s.Length == 6) // RRGGBB
                                return Color.FromArgb(255,
                                    Convert.ToInt32(s.Substring(0, 2), 16),
                                    Convert.ToInt32(s.Substring(2, 2), 16),
                                    Convert.ToInt32(s.Substring(4, 2), 16));
                            if (s.Length == 8) // AARRGGBB
                                return Color.FromArgb(
                                    Convert.ToInt32(s.Substring(0, 2), 16),
                                    Convert.ToInt32(s.Substring(2, 2), 16),
                                    Convert.ToInt32(s.Substring(4, 2), 16),
                                    Convert.ToInt32(s.Substring(6, 2), 16));
                        }
                        // Known color names
                        return Color.FromName(s);
                }
            }
            catch
            {
                // fall through
            }

            // Fallback to default color if provided and valid
            if (defaultValue is Color c)
                return c;

            return Color.Empty;
        }

        /// <summary>
        /// Retrieves a value from the Windows Registry based on the specified key and value name.
        /// </summary>
        /// <remarks>This method supports special handling for certain registry key prefixes: - "HKEY_REAL_CURRENT_USER":
        /// Redirects to the actual current user's registry hive. - "Computer\": Removes this prefix for compatibility. - Other
        /// standard hives (e.g., "HKEY_LOCAL_MACHINE") are handled appropriately based on the system architecture.  If the
        /// retrieved value starts with "#USR:", the method will recursively resolve the value using the
        /// "HKEY_REAL_CURRENT_USER" hive. Logging is performed if application logging is enabled, and errors are logged or
        /// raised based on the <paramref name="RaiseExceptions"/> parameter.</remarks>
        /// <param name="Key">The full registry key path, including the hive (e.g., "HKEY_LOCAL_MACHINE\Software\Example"). Special prefixes such
        /// as "HKEY_REAL_CURRENT_USER" or "Computer\" are supported for specific redirections.</param>
        /// <param name="ValueName">The name of the registry value to retrieve. Use an empty string or <see langword="null"/> to retrieve the default
        /// value of the key.</param>
        /// <param name="DefaultValue">The value to return if the specified registry value does not exist or cannot be retrieved.</param>
        /// <param name="RaiseExceptions">A boolean value indicating whether exceptions should be raised if an error occurs during the registry operation. If
        /// <see langword="true"/>, exceptions will be raised; otherwise, errors will be logged and <paramref
        /// name="DefaultValue"/> will be returned.</param>
        /// <param name="IfNullReturnDefaultValue">A boolean value indicating whether to return <paramref name="DefaultValue"/> if the retrieved registry value is <see
        /// langword="null"/>. If <see langword="true"/>, <paramref name="DefaultValue"/> will be returned when the registry
        /// value is <see langword="null"/>.</param>
        /// <returns>The value retrieved from the registry, or <paramref name="DefaultValue"/> if the value does not exist, is <see
        /// langword="null"/>,  or an error occurs (depending on the value of <paramref name="IfNullReturnDefaultValue"/>).</returns>
        private static object ReadRegRaw(string Key, string ValueName, object DefaultValue, bool RaiseExceptions = false, bool IfNullReturnDefaultValue = true)
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
                    Result = ReadReg($"HKEY_REAL_CURRENT_USER\\{Result.ToString().Replace("#USR:", string.Empty)}", ValueName, DefaultValue, RaiseExceptions, IfNullReturnDefaultValue);
                }

                Program.Log?.WriteRegRead(LogEventLevel.Information, $"(GetReg) `{Key_BeforeModification}` > `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}` returned `{(IfNullReturnDefaultValue && Result is null ? DefaultValue : Result)}`");

                return IfNullReturnDefaultValue && Result is null ? DefaultValue : Result;
            }
            catch (Exception ex)
            {
                Program.Log?.WriteRegRead(LogEventLevel.Error, $"Registry exception error", ex);

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
        /// Retrieves the names of all values under the specified registry key.
        /// </summary>
        /// <remarks>The method supports redirection for certain registry hives, such as "HKEY_CURRENT_USER" and
        /// "HKEY_LOCAL_MACHINE",  to ensure compatibility with different user contexts and system architectures.  If the key
        /// path starts with "Computer\", it will be removed before processing.</remarks>
        /// <param name="Key">The full path of the registry key from which to retrieve value names.  The path can include standard registry hives
        /// such as "HKEY_CURRENT_USER", "HKEY_LOCAL_MACHINE", etc.</param>
        /// <returns>An array of strings containing the names of all values under the specified registry key.  Returns an empty array if
        /// the key does not exist or no values are present.</returns>
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
                        Program.Log?.WriteRegRead(LogEventLevel.Information, $"GetValueNames({Key_BeforeModification}) returned `{string.Join(", ", Result)}`");
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
        /// Retrieves the names of all subkeys within the specified registry key.
        /// </summary>
        /// <remarks>This method supports redirection for certain registry root keys, such as "HKEY_CURRENT_USER" and
        /// "HKEY_LOCAL_MACHINE",  to ensure compatibility with different user contexts and system architectures.  The method
        /// also handles special cases like "HKEY_REAL_CURRENT_USER", which is used internally to distinguish the real current
        /// user.</remarks>
        /// <param name="Key">The full path of the registry key from which to retrieve subkey names.  The path can include standard registry root
        /// keys such as "HKEY_CURRENT_USER" or "HKEY_LOCAL_MACHINE".</param>
        /// <returns>An array of strings containing the names of all subkeys within the specified registry key.  Returns an empty array
        /// if the key does not exist or contains no subkeys.</returns>
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
                        Program.Log?.WriteRegRead(LogEventLevel.Information, $"GetSubKeys({Key_BeforeModification}) `{string.Join(", ", Result)}`");
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
        /// Deletes a specified registry key and optionally retains the key while removing its subkeys and values.
        /// </summary>
        /// <remarks>This method attempts to delete the specified registry key using .NET Framework methods. If the
        /// deletion fails due to security or access issues, the method will fall back to using an alternative approach via the
        /// `REG.EXE` utility.</remarks>
        /// <param name="Key">The full path of the registry key to delete. The path may include a "Computer\" prefix, which will be removed
        /// automatically.</param>
        /// <param name="deleteSubKeysAndValuesOnly">A boolean value indicating whether to delete only the subkeys and values of the specified key, leaving the key
        /// itself intact. If <see langword="true"/>, the key will be recreated as an empty key after its subkeys and values are
        /// deleted.</param>
        /// <param name="treeView">An optional <see cref="TreeView"/> control used to display verbose information about the deletion process. If <see
        /// langword="null"/>, no verbose information will be displayed.</param>
        public static void DeleteKey(string Key, bool deleteSubKeysAndValuesOnly = false, TreeView treeView = null)
        {
            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Process the key to get the scope and key without the scope.
            (string, RegScope) item = FormatKey(Key_BeforeModification);

            // Key without the scope.
            Key = item.Item1;

            // Scope of the key.
            RegScope scope = item.Item2;

            // Open the processed key
            RegistryKey R = OpenBaseKey(scope);

            try
            {
                Program.Log?.WriteRegDel(LogEventLevel.Information, $"Deleting registry key: {Key_BeforeModification}");
                R.DeleteSubKeyTree(Key, true);
                if (deleteSubKeysAndValuesOnly)
                {
                    Program.Log?.WriteRegDel(LogEventLevel.Information, $"Keeping the key intact and empty without subkeys and values.");
                    R.CreateSubKey(Key, true);
                }
                AddVerboseItem_DelKey(treeView, Key_BeforeModification);
            }
            catch
            {
                Program.Log?.WriteRegDel(LogEventLevel.Error, $"Couldn't delete key `{Key_BeforeModification}` using .NET Framework methods. WinPaletter will use REG.EXE");
                // An exception occurred while trying to delete the key. Try to use the DelKey_AdministratorDeflector function to solve the security access issues.
                DeleteKeyAsAdministrator(Key);
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
        /// Deletes a registry key and optionally its subkeys and values, using the specified <see cref="TreeView"/> for
        /// context.
        /// </summary>
        /// <remarks>This method delegates the deletion operation to an internal implementation, using the
        /// provided  <paramref name="treeView"/> for additional context. Ensure the caller has the necessary
        /// permissions  to modify the specified registry key.</remarks>
        /// <param name="treeView">The <see cref="TreeView"/> control used to provide context for the operation.</param>
        /// <param name="Key">The path of the registry key to delete. Cannot be null or empty.</param>
        /// <param name="deleteSubKeysAndValuesOnly">A boolean value indicating whether to delete only the subkeys and values of the specified key,  leaving the
        /// key itself intact. If <see langword="false"/>, the key and all its contents are deleted.</param>
        public static void DeleteKey(TreeView treeView, string Key, bool deleteSubKeysAndValuesOnly = false)
        {
            DeleteKey(Key, deleteSubKeysAndValuesOnly, treeView);
        }

        /// <summary>
        /// Deletes a specified value from a registry key.
        /// </summary>
        /// <remarks>This method attempts to delete the specified value from the registry key. If the operation fails due
        /// to insufficient permissions, it will attempt to use an elevated process to perform the deletion.</remarks>
        /// <param name="Key">The full path of the registry key from which the value will be deleted. The path may include a scope prefix such as
        /// "Computer\".</param>
        /// <param name="ValueName">The name of the value to delete. If null or empty, the default value of the key will be deleted.</param>
        /// <param name="treeView">An optional <see cref="TreeView"/> control to which verbose logging information will be added. If null, no verbose
        /// logging is performed.</param>
        public static void DeleteValue(string Key, string ValueName, TreeView treeView = null)
        {
            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Process the key to get the scope and key without the scope.
            (string, RegScope) item = FormatKey(Key_BeforeModification);

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
                    Program.Log?.WriteRegDel(LogEventLevel.Information, $"(Registry DelValue) `{(string.IsNullOrWhiteSpace(ValueName) ? "(Default)" : ValueName)}` from `{Key_BeforeModification}`.");
                    subR?.DeleteValue(ValueName, true);
                    subR?.Close();
                    AddVerboseItem_DelValue(treeView, Key_BeforeModification, ValueName);
                }
            }
            catch
            {
                Program.Log?.WriteRegDel(LogEventLevel.Error, $"Couldn't delete value using .NET Framework methods. WinPaletter will use REG.EXE");

                // An exception occurred while trying to delete the value. Try to use the DelValue_AdministratorDeflector function to solve the security access issues.
                DeleteValueAsAdministrator(Key, ValueName);
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
        /// Deletes a specific value from a key in the provided <see cref="TreeView"/>.
        /// </summary>
        /// <remarks>This method removes the specified value from the given key in the context of the provided <see
        /// cref="TreeView"/>. Ensure that both <paramref name="Key"/> and <paramref name="ValueName"/> are valid and exist in
        /// the context of the operation.</remarks>
        /// <param name="treeView">The <see cref="TreeView"/> control associated with the operation.</param>
        /// <param name="Key">The key from which the value will be deleted. Cannot be null or empty.</param>
        /// <param name="ValueName">The name of the value to delete. Cannot be null or empty.</param>
        public static void DeleteValue(TreeView treeView, string Key, string ValueName)
        {
            DeleteValue(Key, ValueName, treeView);
        }

        /// <summary>
        /// Determines whether a specified registry key exists.
        /// </summary>
        /// <remarks>The method processes the provided registry key path to determine its scope and checks for the 
        /// existence of the key within the appropriate registry hive. If the key does not exist or an  error occurs during the
        /// check, the method returns <see langword="false"/>.</remarks>
        /// <param name="Key">The full path of the registry key to check. The path may optionally start with "Computer\",  which will be removed
        /// automatically during processing.</param>
        /// <returns><see langword="true"/> if the specified registry key exists; otherwise, <see langword="false"/>.</returns>
        public static bool KeyExists(string Key)
        {
            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Count());

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Process the key to get the scope and key without the scope.
            (string, RegScope) item = FormatKey(Key_BeforeModification);

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
        /// Determines whether a specified registry value exists within a given registry key.
        /// </summary>
        /// <remarks>The method processes the provided registry key path to determine its scope and opens the
        /// corresponding registry key for reading. If the key exists, it checks for the presence of the specified value. If the
        /// value exists and is not null or empty, the method returns <see langword="true"/>; otherwise, it returns <see
        /// langword="false"/>.</remarks>
        /// <param name="Key">The full path of the registry key to check. This may include a scope prefix such as "Computer\".</param>
        /// <param name="ValueName">The name of the registry value to check for existence.</param>
        /// <returns><see langword="true"/> if the specified registry value exists and contains a non-empty value; otherwise, <see
        /// langword="false"/>.</returns>
        public static bool ValueExists(string Key, string ValueName)
        {
            // Removes "Computer\" from the beginning of the key if it exists.
            if (Key.StartsWith(@"Computer\", StringComparison.OrdinalIgnoreCase)) Key = Key.Remove(0, @"Computer\".Length);

            // Key before modification is used to show the original key in the theme log.
            string Key_BeforeModification = Key;

            // Process the key to get the scope and key without the scope.
            (string, RegScope) item = FormatKey(Key_BeforeModification);

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
        /// Deletes a specified registry value using administrative privileges.
        /// </summary>
        /// <remarks>This method uses the REG.EXE command-line utility to delete the specified registry value.  It
        /// requires administrative privileges to execute successfully. Ensure the caller has the necessary
        /// permissions.</remarks>
        /// <param name="Key">The registry key path containing the value to delete. This must be a valid registry key path.</param>
        /// <param name="ValueName">The name of the registry value to delete. This must not be null or empty.</param>
        public static void DeleteValueAsAdministrator(string Key, string ValueName)
        {
            Program.Log?.WriteRegDel(LogEventLevel.Information, $"Deleting registry value using REG.EXE: reg {$@"delete ""{FormatKey_CMD(Key)}\{ValueName}"" /f"}");

            // /f = Disable prompt
            Program.SendCommand($"reg {$@"delete ""{FormatKey_CMD(Key)}\{ValueName}"" /f"}");
        }

        /// <summary>
        /// Deletes a specified registry key using administrative privileges.
        /// </summary>
        /// <remarks>This method uses the REG.EXE command-line utility to delete the specified registry key. 
        /// Administrative privileges are required to execute this operation successfully.  If logging is enabled in the
        /// application settings, a log entry will be created for this action.</remarks>
        /// <param name="Key">The full path of the registry key to delete. This must be a valid registry key path.</param>
        public static void DeleteKeyAsAdministrator(string Key)
        {
            Program.Log?.WriteRegDel(LogEventLevel.Information, $"Deleting registry key using REG.EXE: reg {$@"delete ""{FormatKey_CMD(Key)}"" /f"}");

            // /f = Disable prompt
            Program.SendCommand($"reg {$@"delete ""{FormatKey_CMD(Key)}"" /f"}");
        }

        /// <summary>
        /// Executes the System File Checker (SFC) utility to scan and repair system files.
        /// </summary>
        /// <remarks>This method uses the SFC utility to verify the integrity of system files. If a specific file is
        /// provided and exists,  the method will scan that file. If the file does not exist and <paramref
        /// name="IfNotExist_DoScannow"/> is set to  <see langword="true"/>, a full system scan will be performed instead. The
        /// method requires administrative privileges  to execute successfully.</remarks>
        /// <param name="File">The path to the specific file to scan using SFC. If the file does not exist and  <paramref
        /// name="IfNotExist_DoScannow"/> is set to <see langword="true"/>, a full system scan will be performed.</param>
        /// <param name="IfNotExist_DoScannow">A value indicating whether to perform a full system scan if the specified <paramref name="File"/> does not exist. If
        /// <see langword="false"/>, the method will exit without performing any action if the file is not found.</param>
        /// <param name="Hide">A value indicating whether to hide the command prompt window during the execution of the SFC utility. If <see
        /// langword="true"/>, the window will be hidden; otherwise, it will be visible.</param>
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

                    Program.Log?.Write(LogEventLevel.Information, $"Starting SFC scan for file: {File}");
                    Program.Log?.Write(LogEventLevel.Information, $"The command is: {process.StartInfo.Arguments}");

                    // Start the process and wait for it to finish.
                    process.Start();
                    process.WaitForExit();

                    Program.Log?.Write(LogEventLevel.Information, $"SFC scan finished for file: {File}");
                }

                // Restore the file system redirection.
                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
            }
        }

        /// <summary>
        /// Takes ownership of the specified file and optionally sets the ownership as an administrator.
        /// </summary>
        /// <remarks>This method attempts to take ownership of the specified file using system commands.  If the file
        /// exists, it also tries to set access control to the current user using .NET methods.  If the file does not exist, an
        /// error is logged, and no action is taken.</remarks>
        /// <param name="File">The full path of the file for which ownership is to be taken. The file must exist.</param>
        /// <param name="AsAdministrator">A value indicating whether the ownership should be taken as an administrator.  true to take ownership as an
        /// administrator; otherwise, false.</param>
        public static void TakeOwn_File(string File, bool AsAdministrator = false)
        {
            if (System.IO.File.Exists(File))
            {
                Program.Log?.Write(LogEventLevel.Information, $"Taking ownership of file: {File}");
                Program.Log?.Write(LogEventLevel.Information, $"The command is: {SysPaths.TakeOwn} {string.Format("/f \"{0}\"", File, AsAdministrator ? " /a" : string.Empty)}");
                Program.Log?.Write(LogEventLevel.Information, $"TakeOwn as Administrator: {AsAdministrator}");

                Program.SendCommand($"{SysPaths.TakeOwn} {string.Format("/f \"{0}\"", File, AsAdministrator ? " /a" : string.Empty)}");

                // Try to set the access control to the current user.
                try
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Setting access control to the current user using .NET Framework methods too: {File}");

                    FileSecurity fSecurity = System.IO.File.GetAccessControl(File);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(User.Identity.Name, FileSystemRights.FullControl, AccessControlType.Allow));
                    System.IO.File.SetAccessControl(File, fSecurity);
                }
                catch (Exception ex) // Couldn't set the access control.
                {
                    Program.Log?.Write(LogEventLevel.Error, $"Couldn't set the access control using .NET Framework methods.");

                    Forms.BugReport.ThrowError(ex);
                }
            }
            else
            {
                Program.Log?.Write(LogEventLevel.Error, $"The file does not exist: {File}, so the ownership can't be taken.");
            }
        }

        /// <summary>
        /// Grants full control permissions to the specified file for the current user or administrators group.
        /// </summary>
        /// <remarks>This method uses the ICACLS command-line tool to modify file permissions. If <paramref
        /// name="AsAdministrator"/>  is <see langword="true"/>, the permissions are granted to the administrators group.
        /// Otherwise, permissions  are granted to the current user. Additionally, the method attempts to set access control
        /// using .NET Framework  methods for the current user. <para> Ensure that the file specified by <paramref name="File"/>
        /// exists before calling this method. If the file does  not exist, no action will be taken. </para> <para> This method
        /// may require elevated privileges to execute successfully, depending on the file's current permissions  and the value
        /// of <paramref name="AsAdministrator"/>. </para></remarks>
        /// <param name="File">The path to the file for which permissions will be modified. The file must exist.</param>
        /// <param name="AsAdministrator">A boolean value indicating whether to grant permissions to the administrators group  (<see langword="true"/>) or the
        /// current user (<see langword="false"/>).</param>
        public static void ICACLS(string File, bool AsAdministrator = false)
        {
            if (System.IO.File.Exists(File))
            {
                Program.Log?.Write(LogEventLevel.Information, $"Taking ownership of file: {File}");
                Program.Log?.Write(LogEventLevel.Information, $"The command is: {SysPaths.System32}\\ICACLS.exe {$"\"{File}\" /grant {(AsAdministrator ? "administrators" : "%username%")}:F"}");
                Program.Log?.Write(LogEventLevel.Information, $"ICACLS as Administrator: {AsAdministrator}");

                Program.SendCommand($"{SysPaths.System32}\\ICACLS.exe {$"\"{File}\" /grant {(AsAdministrator ? "administrators" : "%username%")}:F"}");

                // Try to set the access control to the current user.
                try
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Setting access control to the current user using .NET Framework methods too: {File}");

                    FileSecurity fSecurity = System.IO.File.GetAccessControl(File);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(User.Identity.Name, FileSystemRights.FullControl, AccessControlType.Allow));
                    System.IO.File.SetAccessControl(File, fSecurity);
                }
                catch { }
            }
        }

        /// <summary>
        /// Moves a file from the specified source path to the specified destination path.
        /// </summary>
        /// <remarks>This method uses a command prompt operation to move the file, which may help resolve security or
        /// administrator access issues. Ensure that the source file exists and that the application has the necessary
        /// permissions to access both the source and destination paths.</remarks>
        /// <param name="source">The full path of the file to be moved. This must be a valid file path and the file must exist.</param>
        /// <param name="destination">The full path where the file should be moved. This must be a valid destination path.</param>
        public static void Move_File(string source, string destination)
        {
            if (File.Exists(source))
            {
                Program.Log?.Write(LogEventLevel.Information, $"Moving file `{source}` to `{destination}`");
                Program.Log?.Write(LogEventLevel.Information, $"The command is: {SysPaths.CMD} /C move \"{source}\" \"{destination}\" && exit");
                Program.Log?.Write(LogEventLevel.Information, $"Command prompt is used to move the file to try to solve security access issues or administrator issues.");

                Program.SendCommand($"{SysPaths.CMD} /C move \"{source}\" \"{destination}\" && exit");
            }
        }
    }
}