using Newtonsoft.Json.Linq;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Localizer : IDisposable
    {
        #region IDisposable Support
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Variables

        // Grouped by form name (lowercased) for O(1) lookup instead of scanning the flat list.
        private Dictionary<string, List<(string ControlName, string Prop, string Value)>> _treeByForm = [with(StringComparer.OrdinalIgnoreCase)];

        /// <summary>
        /// Information about the current language
        /// </summary>
        public Information_Cls Information { get; set; } = new();

        /// <summary>
        /// All strings used in the current application
        /// </summary>
        public Strings_Cls Strings { get; set; } = new();

        public JObject Forms { get; set; } = [];

        #endregion

        /// <summary>
        /// Creates a new instance of the Localizer class
        /// </summary>
        public Localizer() { }

        /// <summary>
        /// Load the language File
        /// </summary>
        public void Load(string File, System.Windows.Forms.Form form = null)
        {
            if (!System.IO.File.Exists(File)) return;

            Program.Log?.Write(LogEventLevel.Information, $"Loading language from file `{File}`.");

            JObject JObj;

            using (StreamReader St = new(File))
            {
                JObj = JObject.Parse(St.ReadToEnd());
            }

            Information = new();
            Strings = new();
            Forms = [];
            _treeByForm = [with(StringComparer.OrdinalIgnoreCase)];

            if (!JObj.ContainsKey(nameof(Information)) || !JObj.ContainsKey("Global Strings") || !JObj.ContainsKey("Forms Strings")) return;

            Information = JObj[nameof(Information)].ToObject<Information_Cls>();
            Program.Log?.Write(LogEventLevel.Information, "Information of language file have been loaded.");

            Strings = JObj["Global Strings"].ToObject<Strings_Cls>();
            Program.Log?.Write(LogEventLevel.Information, "Global strings inside language file have been loaded.");

            Forms = (JObject)JObj["Forms Strings"];

            DeserializeFormsJSONIntoDict(Forms, _treeByForm);

            LoadFromStrings(form);
            Program.Log?.Write(LogEventLevel.Information, "Forms strings inside language file have been loaded.");
        }

        /// <summary>
        /// Save the language File as JSON format
        /// </summary>
        public void Save(string File, System.Windows.Forms.Form[] Forms = null)
        {
            Information.AppVer = Program.Version;

            JObject JSON_Overall = [];
            JObject j_Forms = [];

            if (Forms == null)
            {
                foreach (Type t in WinPaletter.Forms.ITypes)
                {
                    using (System.Windows.Forms.Form form = Activator.CreateInstance(t) as System.Windows.Forms.Form)
                    {
                        j_Forms.Add(form.Name, form.ToJSON());
                    }
                }
            }
            else
            {
                if (System.IO.File.Exists(File))
                {
                    JObject oldSource = JObject.Parse(System.IO.File.ReadAllText(File));
                    j_Forms = oldSource["Forms Strings"] as JObject ?? [];
                }

                foreach (System.Windows.Forms.Form form in Forms)
                {
                    if (j_Forms.ContainsKey(form.Name))
                        j_Forms[form.Name] = form.ToJSON();
                    else
                        j_Forms.Add(form.Name, form.ToJSON());
                }
            }

            JSON_Overall.Add(nameof(Information), JObject.FromObject(Information));
            JSON_Overall.Add("Global Strings", JObject.FromObject(Strings));
            JSON_Overall.Add("Forms Strings", j_Forms);

            System.IO.File.WriteAllText(File, JSON_Overall.ToString());
        }

        /// <summary>
        /// Save the language File as JSON format
        /// </summary>
        public void Save(string File, JObject formsJObject)
        {
            Information.AppVer = Program.Version;
            JObject JSON_Overall = [];

            JSON_Overall.Add(nameof(Information), JObject.FromObject(Information));
            JSON_Overall.Add("Global Strings", JObject.FromObject(Strings));
            JSON_Overall.Add("Forms Strings", formsJObject);

            System.IO.File.WriteAllText(File, JSON_Overall.ToString());
        }

        // Reads a "Text" value from a JObject using case-insensitive key matching with a single TryGetValue call.
        private static bool TryGetTextValue(JObject jobj, out string value)
        {
            // JObject property names are case-sensitive; try common casings cheaply before falling back.
            if (jobj.TryGetValue("Text", StringComparison.Ordinal, out JToken tok) || jobj.TryGetValue("text", StringComparison.Ordinal, out tok) || jobj.TryGetValue("TEXT", StringComparison.Ordinal, out tok))
            {
                value = tok.ToString();
                return true;
            }
            value = null;
            return false;
        }

        // Reads a "Controls" sub-object using case-insensitive key matching.
        private static bool TryGetControls(JObject jobj, out JObject controls)
        {
            if (jobj.TryGetValue("Controls", StringComparison.Ordinal, out JToken tok) || jobj.TryGetValue("controls", StringComparison.Ordinal, out tok) || jobj.TryGetValue("CONTROLS", StringComparison.Ordinal, out tok))
            {
                controls = tok as JObject;
                return controls != null;
            }
            controls = null;
            return false;
        }

        // Shared deserialization core. Populates the target dictionary keyed by form name.
        private static void DeserializeFormsJSONIntoDict(JObject JSON_Forms, Dictionary<string, List<(string ControlName, string Prop, string Value)>> target)
        {
            foreach (KeyValuePair<string, JToken> F in JSON_Forms)
            {
                string formName = F.Key;
                JObject J_Specific_Form = F.Value as JObject;
                if (J_Specific_Form == null) continue;

                if (!target.TryGetValue(formName, out List<(string, string, string)> entries))
                {
                    entries = [];
                    target[formName] = entries;
                }

                if (TryGetTextValue(J_Specific_Form, out string formText)) entries.Add((string.Empty, "Text", formText));

                if (TryGetControls(J_Specific_Form, out JObject J_Controls))
                {
                    foreach (KeyValuePair<string, JToken> ctrl in J_Controls)
                    {
                        string value = ctrl.Value?.ToString() ?? string.Empty;

                        if (ctrl.Key.IndexOf('.') >= 0)
                        {
                            int dot = ctrl.Key.IndexOf('.');
                            entries.Add((ctrl.Key.Substring(0, dot), ctrl.Key.Substring(dot + 1), value));
                        }
                        else
                        {
                            entries.Add((ctrl.Key, "Text", value));
                        }
                    }
                }
            }
        }

        // Deserializes a single form's JSON node. Used when reloading one form from an external JObject.
        private static List<(string ControlName, string Prop, string Value)> DeserializeOneFormJSON(string formName, JObject JSON_Form)
        {
            if (JSON_Form == null) return null;

            Dictionary<string, List<(string, string, string)>> temp = new(1, StringComparer.OrdinalIgnoreCase);
            JObject wrapper = new() { [formName] = JSON_Form };
            DeserializeFormsJSONIntoDict(wrapper, temp);

            return temp.TryGetValue(formName, out List<(string, string, string)> entries) ? entries : null;
        }

        /// <summary>
        /// Load the language strings into the form using the preloaded tree.
        /// </summary>
        public void LoadFromStrings(System.Windows.Forms.Form form)
        {
            if (form == null) return;

            if (!_treeByForm.TryGetValue(form.Name, out List<(string ControlName, string Prop, string Value)> entries)) return;

            ApplyEntriesToForm(entries, form);
        }

        /// <summary>
        /// Load the language strings into the form from a JObject.
        /// </summary>
        public void LoadFromStrings(System.Windows.Forms.Form form, JObject jObject)
        {
            if (form == null) return;

            List<(string ControlName, string Prop, string Value)> entries = DeserializeOneFormJSON(form.Name, jObject);
            if (entries == null) return;

            ApplyEntriesToForm(entries, form);
        }

        /// <summary>
        /// Load the language of a form.
        /// </summary>
        public static void LoadLanguage(System.Windows.Forms.Form Form, JObject jObject, Localizer Localizer)
        {
            Localizer?.LoadFromStrings(Form, jObject);
        }

        // Builds a name→control lookup once per call to avoid repeated Controls.Find() tree walks.
        private static Dictionary<string, Control> BuildControlMap(System.Windows.Forms.Form form)
        {
            // Controls.Find with empty string returns nothing on some WinForms versions; use recursive helper instead.
            Dictionary<string, Control> map = [with(StringComparer.OrdinalIgnoreCase)];
            CollectControls(form.Controls, map);
            return map;
        }

        private static void CollectControls(Control.ControlCollection controls, Dictionary<string, Control> map)
        {
            foreach (Control c in controls)
            {
                if (!string.IsNullOrEmpty(c.Name) && !map.ContainsKey(c.Name)) map[c.Name] = c;

                if (c.HasChildren) CollectControls(c.Controls, map);
            }
        }

        // Applies a pre-parsed entry list to the target form.
        // Hides the form only once, builds the control map once, then applies all entries.
        private static void ApplyEntriesToForm(List<(string ControlName, string Prop, string Value)> entries, System.Windows.Forms.Form form)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Setting strings for form `{form.Name}`.");

            bool wasVisible = form.Visible;
            if (wasVisible) form.Visible = false;

            // Build name→control map once so we never call Controls.Find() inside the loop.
            Dictionary<string, Control> controlMap = BuildControlMap(form);

            foreach ((string controlName, string prop, string value) in entries)
            {
                string propLower = prop.ToLowerInvariant();

                if (string.IsNullOrEmpty(controlName))
                {
                    // Form-level property
                    if (propLower == "text") form.SetText(value ?? string.Empty);
                    else if (propLower == "tag") form.SetTag(value ?? string.Empty);
                }
                else if (controlMap.TryGetValue(controlName, out Control ctrl))
                {
                    if (propLower == "text") ctrl.SetText(value);
                    else if (propLower == "tag") ctrl.SetTag(value);
                }
            }

            if (wasVisible) form.Visible = true;
        }
    }
}