using Newtonsoft.Json.Linq;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Manages localization data loading, saving, and application to forms.
    /// Uses System.Reflection + raw IL parsing for string extraction without form instantiation.
    /// </summary>
    public partial class Localizer : IDisposable
    {
        private Dictionary<string, List<FormStringEntry>> _treeByForm = [with(StringComparer.OrdinalIgnoreCase)];
        private static readonly Regex SingleLetterWithPunctuationRegex = new(@"^[\p{P}]*[A-Za-z][\p{P}]*$", RegexOptions.Compiled);
        private static readonly Regex VersionRegex = new Regex(@"^\d+(\.\d+){1,3}$", RegexOptions.Compiled);

        // Opcode byte values used in IL parsing (single-byte opcodes only needed here)
        private const byte OP_LDSTR = 0x72; // ldstr
        private const byte OP_CALLVIRT = 0x6F; // callvirt
        private const byte OP_CALL = 0x28; // call
        private const byte OP_LDFLD = 0x7B; // ldfld
        private const byte OP_LDARG_0 = 0x02; // ldarg.0
        private const byte OP_PREFIX1 = 0xFE; // prefix for two-byte opcodes (skip them)

        public Information_Cls Information { get; set; } = new Information_Cls();
        public Strings_Cls Strings { get; set; } = new Strings_Cls();
        public JObject Forms { get; set; } = [];

        #region Constructor

        public Localizer() { }

        /// <summary>
        /// Represents a single localizable string entry from a form or control.
        /// Immutable value type for performance and clarity.
        /// </summary>
        public readonly struct FormStringEntry
        {
            public string ControlName { get; }
            public string PropertyName { get; }
            public string Value { get; }

            public FormStringEntry(string controlName, string propertyName, string value)
            {
                ControlName = controlName ?? string.Empty;
                PropertyName = propertyName ?? "Text";
                Value = value ?? string.Empty;
            }

            public bool IsFormEntry => string.IsNullOrEmpty(ControlName);
            public bool IsControlEntry => !string.IsNullOrEmpty(ControlName);

            public static FormStringEntry FormEntry(string propertyName, string value)
                => new FormStringEntry(string.Empty, propertyName, value);

            public static FormStringEntry ControlEntry(string controlName, string propertyName, string value)
                => new FormStringEntry(controlName, propertyName, value);

            public override string ToString()
                => IsFormEntry ? $"Form.{PropertyName}: {Value}" : $"{ControlName}.{PropertyName}: {Value}";

            public override bool Equals(object obj)
                => obj is FormStringEntry other
                    && ControlName == other.ControlName
                    && PropertyName == other.PropertyName
                    && Value == other.Value;

            public override int GetHashCode()
                => HashCode.Combine(ControlName, PropertyName, Value);

            public static bool operator ==(FormStringEntry left, FormStringEntry right) => left.Equals(right);
            public static bool operator !=(FormStringEntry left, FormStringEntry right) => !left.Equals(right);
        }

        #endregion

        #region Load Methods

        public void Load(string file, System.Windows.Forms.Form form = null)
        {
            if (!File.Exists(file)) return;

            Program.Log?.Write(LogEventLevel.Information, $"Loading language from file `{file}`.");

            JObject jObj;
            using (var sr = new StreamReader(file))
            {
                jObj = JObject.Parse(sr.ReadToEnd());
            }

            Information = new();
            Strings = new();
            Forms = [];
            _treeByForm = [with(StringComparer.OrdinalIgnoreCase)];

            if (!jObj.ContainsKey("Information") || !jObj.ContainsKey("Global Strings") || !jObj.ContainsKey("Forms Strings")) return;

            Information = jObj["Information"].ToObject<Information_Cls>();
            Strings = jObj["Global Strings"].ToObject<Strings_Cls>();
            Forms = jObj["Forms Strings"] as JObject ?? [];

            DeserializeFormsJSONIntoDict(Forms, _treeByForm);

            if (form != null) ApplyLocalization(form);
        }

        #endregion

        #region Save Methods

        public void Save(string file)
        {
            Information.AppVer = Program.Version;

            var json = new JObject();
            var jForms = ExtractAllFormsViaReflection();

            json["Information"] = JObject.FromObject(Information);
            json["Global Strings"] = JObject.FromObject(Strings);
            json["Forms Strings"] = jForms;

            File.WriteAllText(file, json.ToString());
        }

        public void Save(string file, System.Windows.Forms.Form[] forms)
        {
            Information.AppVer = Program.Version;

            var json = new JObject();
            var jForms = new JObject();

            if (File.Exists(file))
            {
                var old = JObject.Parse(File.ReadAllText(file));
                jForms = old["Forms Strings"] as JObject ?? new JObject();
            }

            foreach (var f in forms)
            {
                jForms[f.Name] = f.JObject();
            }

            json["Information"] = JObject.FromObject(Information);
            json["Global Strings"] = JObject.FromObject(Strings);
            json["Forms Strings"] = jForms;

            File.WriteAllText(file, json.ToString());
        }

        public void Save(string file, JObject formsJObject)
        {
            Information.AppVer = Program.Version;

            var json = new JObject
            {
                [nameof(Information)] = JObject.FromObject(Information),
                ["Global Strings"] = JObject.FromObject(Strings),
                ["Forms Strings"] = formsJObject
            };

            File.WriteAllText(file, json.ToString());
        }

        public void SaveHybrid(string file, System.Windows.Forms.Form[] dirtyForms = null)
        {
            Information.AppVer = Program.Version;

            var jForms = ExtractAllFormsViaReflection();

            if (dirtyForms != null)
            {
                foreach (var form in dirtyForms)
                {
                    jForms[form.Name] = form.JObject();
                }
            }

            var json = new JObject
            {
                ["Information"] = JObject.FromObject(Information),
                ["Global Strings"] = JObject.FromObject(Strings),
                ["Forms Strings"] = jForms
            };

            File.WriteAllText(file, json.ToString());
        }

        #endregion

        #region Reflection-based IL Extraction

        /// <summary>
        /// Extracts localizable strings from all form types using System.Reflection + raw IL parsing.
        /// No form instantiation, no external dependencies.
        /// </summary>
        private JObject ExtractAllFormsViaReflection()
        {
            var jForms = new JObject();
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (Type type in WinPaletter.Forms.ITypes)
            {
                try
                {
                    JObject formObj = ExtractFormStringsViaIL(type, assembly);
                    jForms[type.Name] = formObj;
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error,
                        $"Failed to extract strings from {type.Name}: {ex.Message}");
                }
            }

            return jForms;
        }

        /// <summary>
        /// Extracts localizable strings from a single type by parsing raw IL bytes.
        ///
        /// IL instruction layout (simplified for single-byte opcodes):
        ///   [opcode: 1 byte] [operand: 0–8 bytes depending on opcode]
        ///
        /// We scan for the pattern:
        ///   ldfld  <FieldReference>        -- identifies the control field (optional, may be absent for this.Text)
        ///   ldstr  <string token>          -- the string being assigned
        ///   callvirt set_Text / set_Tag    -- the property setter
        ///
        /// For form-level this.Text:
        ///   ldarg.0
        ///   ldstr  <string token>
        ///   callvirt set_Text
        ///
        /// Token resolution uses Module.ResolveString(token) and Module.ResolveMethod(token).
        /// </summary>
        private JObject ExtractFormStringsViaIL(Type type, Assembly assembly)
        {
            var formObj = new JObject();
            var controls = new JObject();

            Module module = type.Module;

            // Collect all methods declared on this type (not inherited)
            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Instance | BindingFlags.Static |
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                MethodBody body;
                try { body = method.GetMethodBody(); }
                catch { continue; }

                if (body == null) continue;

                byte[] il = body.GetILAsByteArray();
                if (il == null || il.Length == 0) continue;

                // Parsed instruction list: (offset, opcode_byte, int32_operand_or_-1)
                // We only track offsets and 4-byte token operands (ldstr, call, callvirt, ldfld)
                List<(int offset, byte opcode, int token)> instructions = ParseILInstructions(il);

                string lastFieldName = null;
                bool lastWasLdarg0 = false;

                for (int i = 0; i < instructions.Count - 1; i++)
                {
                    byte op = instructions[i].opcode;
                    int tok = instructions[i].token;

                    // Track ldfld to capture the field (control) name
                    if (op == OP_LDFLD && tok != -1)
                    {
                        try
                        {
                            FieldInfo field = module.ResolveField(tok);
                            lastFieldName = field?.Name;
                        }
                        catch { lastFieldName = null; }
                        lastWasLdarg0 = false;
                        continue;
                    }

                    // Track ldarg.0 for this.Text = "..." pattern
                    if (op == OP_LDARG_0)
                    {
                        lastWasLdarg0 = true;
                        lastFieldName = null;
                        continue;
                    }

                    // ldstr: resolve the string token
                    if (op == OP_LDSTR && tok != -1)
                    {
                        string value;
                        try { value = module.ResolveString(tok); }
                        catch { ResetTracking(ref lastFieldName, ref lastWasLdarg0); continue; }

                        if (string.IsNullOrWhiteSpace(value))
                        {
                            ResetTracking(ref lastFieldName, ref lastWasLdarg0);
                            continue;
                        }

                        // Next instruction must be callvirt set_Text or set_Tag
                        if (i + 1 >= instructions.Count)
                        {
                            ResetTracking(ref lastFieldName, ref lastWasLdarg0);
                            continue;
                        }

                        byte nextOp = instructions[i + 1].opcode;
                        int nextTok = instructions[i + 1].token;

                        if ((nextOp == OP_CALLVIRT || nextOp == OP_CALL) && nextTok != -1)
                        {
                            MethodBase setter;
                            try { setter = module.ResolveMethod(nextTok); }
                            catch { ResetTracking(ref lastFieldName, ref lastWasLdarg0); continue; }

                            if (setter == null)
                            {
                                ResetTracking(ref lastFieldName, ref lastWasLdarg0);
                                continue;
                            }

                            string setterName = setter.Name;

                            if (setterName != "set_Text" && setterName != "set_Tag")
                            {
                                ResetTracking(ref lastFieldName, ref lastWasLdarg0);
                                continue;
                            }

                            string propertyName = setterName == "set_Text" ? "Text" : "Tag";

                            if (lastWasLdarg0 && string.IsNullOrEmpty(lastFieldName))
                            {
                                // this.Text = value
                                if (propertyName == "Text" && IsTranslatable(value, null))
                                {
                                    if (!formObj.ContainsKey("Text"))
                                        formObj["Text"] = value;
                                }
                            }
                            else if (!string.IsNullOrEmpty(lastFieldName))
                            {
                                if (IsTranslatable(value, lastFieldName))
                                {
                                    string key = $"{lastFieldName}.{propertyName}";
                                    if (!controls.ContainsKey(key))
                                        controls[key] = value;
                                }
                            }
                        }

                        ResetTracking(ref lastFieldName, ref lastWasLdarg0);
                        continue;
                    }

                    // Any other instruction resets tracking state
                    // (Except: allow a few "pass-through" opcodes that appear between
                    //  ldfld and ldstr in designer code, e.g. ldarg.0 chains)
                    if (op != OP_LDARG_0 && op != OP_LDFLD)
                    {
                        ResetTracking(ref lastFieldName, ref lastWasLdarg0);
                    }
                }
            }

            if (!formObj.ContainsKey("Text"))
                formObj["Text"] = string.Empty;

            formObj["Controls"] = controls;
            return formObj;
        }

        private static void ResetTracking(ref string lastFieldName, ref bool lastWasLdarg0)
        {
            lastFieldName = null;
            lastWasLdarg0 = false;
        }

        /// <summary>
        /// Parses raw IL bytes into a flat list of (offset, opcode, int32Token) tuples.
        ///
        /// Only single-byte opcodes are needed. Two-byte opcodes (prefix 0xFE) are skipped
        /// with their operand. All 4-byte operands are read as little-endian int32.
        ///
        /// Opcode operand sizes (subset relevant to scanning):
        ///   ldarg.0  (0x02) — no operand
        ///   ldstr    (0x72) — 4 bytes (metadata token)
        ///   ldfld    (0x7B) — 4 bytes (metadata token)
        ///   call     (0x28) — 4 bytes (metadata token)
        ///   callvirt (0x6F) — 4 bytes (metadata token)
        ///
        /// All other opcodes are advanced by their standard operand size so the
        /// stream stays in sync. Unknown opcodes fall back to 0-byte operand.
        /// </summary>
        private static List<(int offset, byte opcode, int token)> ParseILInstructions(byte[] il)
        {
            var result = new List<(int, byte, int)>(il.Length / 3);
            int pos = 0;

            while (pos < il.Length)
            {
                int offset = pos;
                byte op = il[pos++];

                // Two-byte opcode prefix — skip the second byte + 4-byte operand
                if (op == OP_PREFIX1)
                {
                    if (pos < il.Length) pos++; // second opcode byte
                    pos += 4;                    // operand (all 0xFE opcodes use 4 bytes or none; safe to skip 4)
                    continue;
                }

                int operandSize = GetOperandSize(op);

                if (operandSize == 4 && pos + 3 < il.Length)
                {
                    int token = il[pos]
                              | (il[pos + 1] << 8)
                              | (il[pos + 2] << 16)
                              | (il[pos + 3] << 24);
                    result.Add((offset, op, token));
                    pos += 4;
                }
                else
                {
                    result.Add((offset, op, -1));
                    pos += operandSize;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the operand byte size for a single-byte opcode.
        /// Only needs to be correct for opcodes we encounter; others advance safely.
        /// Reference: ECMA-335 Partition III Table III.1.
        /// </summary>
        private static int GetOperandSize(byte op)
        {
            switch (op)
            {
                // No operand
                case 0x00:
                case 0x01:
                case 0x02:
                case 0x03:
                case 0x04:
                case 0x05:
                case 0x06:
                case 0x07:
                case 0x08:
                case 0x09:
                case 0x0A:
                case 0x0B:
                case 0x0C:
                case 0x0D:
                case 0x14: // ldnull
                case 0x15: // ldc.i4.m1
                case 0x16:
                case 0x17:
                case 0x18:
                case 0x19:
                case 0x1A:
                case 0x1B:
                case 0x1C:
                case 0x1D:
                case 0x1E:
                case 0x25:
                case 0x26:
                case 0x2A:
                case 0x59:
                case 0x5A:
                case 0x5B:
                case 0x5C:
                case 0x5D:
                case 0x5E:
                case 0x5F:
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                case 0x64:
                case 0x65:
                case 0x66:
                case 0x6A:
                case 0x6B:
                case 0x6C:
                case 0x79: // throw
                    return 0;

                // 1-byte operand
                case 0x0E:
                case 0x0F:
                case 0x10:
                case 0x11:
                case 0x12:
                case 0x13:
                case 0x1F: // ldc.i4.s
                case 0x2B:
                case 0x2C:
                case 0x2D:
                case 0x2E:
                case 0x2F:
                case 0x30:
                case 0x31:
                case 0x32:
                case 0x33:
                case 0x34:
                case 0x35:
                case 0x36:
                case 0x37:
                    return 1;

                // 4-byte operand
                case 0x20:
                case 0x28:
                case 0x29:
                case 0x38:
                case 0x39:
                case 0x3A:
                case 0x3B:
                case 0x3C:
                case 0x3D:
                case 0x3E:
                case 0x3F:
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x6F:
                case 0x70:
                case 0x71:
                case 0x72:
                case 0x73:
                case 0x74:
                case 0x75:
                case 0x77:
                case 0x7B:
                case 0x7C:
                case 0x7D:
                case 0x7E:
                case 0x7F:
                case 0x80:
                case 0x81:
                case 0x8C:
                case 0x8D:
                case 0x8F:
                case 0xA3:
                case 0xA4:
                case 0xB8:
                case 0xD0:
                    return 4;

                // 8-byte operand
                case 0x21:
                case 0x23:
                    return 8;

                // switch
                case 0x45:
                    return 4;

                default:
                    return 0;
            }
        }

        #endregion

        #region Localization Application

        public void ApplyLocalization(System.Windows.Forms.Form form)
        {
            if (form == null) return;

            if (!_treeByForm.TryGetValue(form.Name, out List<FormStringEntry> entries))
                return;

            ApplyEntries(entries, form);
        }

        public void ApplyLocalization(System.Windows.Forms.Form form, JObject localizationJson)
        {
            if (form == null || localizationJson == null) return;

            List<FormStringEntry> entries = DeserializeFormJObjectIntoEntries(localizationJson);
            ApplyEntries(entries, form);
        }

        /// <summary>
        /// Deserializes a single form JObject (already unwrapped — containing "Text" and "Controls"
        /// directly) into a flat list of FormStringEntry values.
        /// Used by ApplyLocalization(form, JObject) which receives the inner per-form object,
        /// not the outer forms collection.
        /// </summary>
        private static List<FormStringEntry> DeserializeFormJObjectIntoEntries(JObject formObj)
        {
            List<FormStringEntry> entries = [];

            if (formObj.TryGetValue("Text", out JToken textToken) && textToken.Type != JTokenType.Null)
            {
                entries.Add(FormStringEntry.FormEntry("Text", textToken.ToString()));
            }

            JObject controlsObj = formObj["Controls"] as JObject;
            if (controlsObj == null) return entries;

            foreach (KeyValuePair<string, JToken> controlProperty in controlsObj)
            {
                string value = controlProperty.Value?.ToString() ?? string.Empty;
                int dotIndex = controlProperty.Key.IndexOf('.');

                if (dotIndex >= 0)
                {
                    string controlName = controlProperty.Key.Substring(0, dotIndex);
                    string propertyName = controlProperty.Key.Substring(dotIndex + 1);
                    entries.Add(FormStringEntry.ControlEntry(controlName, propertyName, value));
                }
                else
                {
                    entries.Add(FormStringEntry.ControlEntry(controlProperty.Key, "Text", value));
                }
            }

            return entries;
        }

        private static void ApplyEntries(List<FormStringEntry> entries, System.Windows.Forms.Form form)
        {
            bool wasVisible = form.Visible;
            if (wasVisible) form.Visible = false;

            try
            {
                Dictionary<string, Control> controlMap = BuildControlMap(form);

                foreach (FormStringEntry entry in entries)
                {
                    if (entry.IsFormEntry)
                    {
                        if (entry.PropertyName.Equals("Text", StringComparison.OrdinalIgnoreCase))
                            form.Text = entry.Value;
                        continue;
                    }

                    if (controlMap.TryGetValue(entry.ControlName, out Control control))
                    {
                        if (entry.PropertyName.Equals("Text", StringComparison.OrdinalIgnoreCase)) control.Text = entry.Value;
                        else if (entry.PropertyName.Equals("Tag", StringComparison.OrdinalIgnoreCase)) control.Tag = entry.Value;
                    }
                }
            }
            finally
            {
                if (wasVisible) form.Visible = true;
            }
        }

        #endregion

        #region JSON Deserialization

        private static void DeserializeFormsJSONIntoDict(JObject jsonForms, Dictionary<string, List<FormStringEntry>> target)
        {
            foreach (var formProperty in jsonForms)
            {
                JObject formObj = formProperty.Value as JObject;
                if (formObj == null) continue;

                if (!target.TryGetValue(formProperty.Key, out List<FormStringEntry> entries))
                {
                    target[formProperty.Key] = entries = new List<FormStringEntry>();
                }

                if (formObj.TryGetValue("Text", out JToken textToken))
                {
                    entries.Add(FormStringEntry.FormEntry("Text", textToken.ToString()));
                }

                JObject controlsObj = formObj["Controls"] as JObject;
                if (controlsObj == null) continue;

                foreach (var controlProperty in controlsObj)
                {
                    string value = controlProperty.Value?.ToString() ?? string.Empty;
                    int dotIndex = controlProperty.Key.IndexOf('.');

                    if (dotIndex >= 0)
                    {
                        string controlName = controlProperty.Key.Substring(0, dotIndex);
                        string propertyName = controlProperty.Key.Substring(dotIndex + 1);
                        entries.Add(FormStringEntry.ControlEntry(controlName, propertyName, value));
                    }
                    else
                    {
                        entries.Add(FormStringEntry.ControlEntry(controlProperty.Key, "Text", value));
                    }
                }
            }
        }

        #endregion

        #region Control Map Building

        private static Dictionary<string, Control> BuildControlMap(System.Windows.Forms.Form form)
        {
            var map = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase);
            CollectControls(form.Controls, map);
            return map;
        }

        private static void CollectControls(Control.ControlCollection controls, Dictionary<string, Control> map)
        {
            foreach (Control control in controls)
            {
                if (!string.IsNullOrEmpty(control.Name) && !map.ContainsKey(control.Name))
                    map[control.Name] = control;

                if (control.HasChildren)
                    CollectControls(control.Controls, map);
            }
        }

        #endregion

        #region Static Helper Methods

        public static void LoadLanguage(System.Windows.Forms.Form form, JObject localizationJson, Localizer localizer)
        {
            localizer?.ApplyLocalization(form, localizationJson);
        }

        public static bool IsTranslatable(string text, string controlName = null)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            if (text.Length < 2) return false;

            if (text.StartsWith("http://", StringComparison.Ordinal) ||
                text.StartsWith("https://", StringComparison.Ordinal))
                return false;

            if (!string.IsNullOrEmpty(controlName) && text == controlName)
                return false;

            int digitCount = 0, letterCount = 0, punctCount = 0, symbolCount = 0;
            int len = text.Length;

            for (int i = 0; i < len; i++)
            {
                char c = text[i];
                if (char.IsDigit(c)) digitCount++;
                else if (char.IsLetter(c)) letterCount++;
                else if (char.IsPunctuation(c)) punctCount++;
                else if (char.IsSymbol(c)) symbolCount++;
            }

            if (digitCount == len) return false;
            if (punctCount == len || symbolCount == len) return false;

            if (letterCount == 1 && (punctCount + letterCount) == len &&
                SingleLetterWithPunctuationRegex.IsMatch(text))
                return false;

            if (len == 36 && Guid.TryParse(text, out _)) return false;

            if (digitCount + punctCount == len && VersionRegex.IsMatch(text)) return false;

            if (digitCount >= 3 &&
                (text.IndexOf('-') >= 0 || text.IndexOf('/') >= 0 || text.IndexOf(':') >= 0))
            {
                if (DateTime.TryParse(text, out _)) return false;
            }

            return true;
        }

        #endregion

        #region IDisposable Support

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _treeByForm?.Clear();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    // LocalizerExtensions remains unchanged — it operates on live Form instances only
    public static class LocalizerExtensions
    {
        public static JObject JObject(this System.Windows.Forms.Form form)
        {
            if (Forms.IExclude.Contains(form.GetType())) return null;

            JObject jControl = new JObject();
            JObject jChildren = new JObject();

            jControl.Add(nameof(form.Text), form.Text);

            IEnumerable<Control> allControls = form.GetAllControls();

            HashSet<IntPtr> tablessChildHandles = BuildTablessControlHandleSet(allControls);

            foreach (Control ctrl in allControls)
            {
                if (tablessChildHandles?.Contains(ctrl.Handle) == true) continue;
                if (ctrl is TabPage) continue;

                ProcessControlForLocalization(ctrl, jChildren);
            }

            if (jChildren.Count > 0) jControl.Add("Controls", jChildren);

            return jControl;
        }

        private static HashSet<IntPtr> BuildTablessControlHandleSet(IEnumerable<Control> allControls)
        {
            HashSet<IntPtr> handleSet = null;

            foreach (var tabControl in allControls.OfType<UI.WP.TablessControl>())
            {
                handleSet ??= new HashSet<IntPtr>();

                foreach (TabPage tabPage in tabControl.TabPages)
                {
                    foreach (Control tabChild in tabPage.Controls)
                    {
                        handleSet.Add(tabChild.Handle);
                    }
                }
            }

            return handleSet;
        }

        private static void ProcessControlForLocalization(Control control, JObject childrenJson)
        {
            string controlTag = control.Tag?.ToString();

            string textKey = $"{control.Name}.{nameof(control.Text)}";
            string tagKey = $"{control.Name}.{nameof(control.Tag)}";

            if (!childrenJson.ContainsKey(textKey) &&
                Localizer.IsTranslatable(control.Text, control.Name))
            {
                childrenJson.Add(textKey, control.Text);
            }

            if (!childrenJson.ContainsKey(tagKey) &&
                Localizer.IsTranslatable(controlTag, control.Name))
            {
                childrenJson.Add(tagKey, controlTag);
            }
        }
    }
}