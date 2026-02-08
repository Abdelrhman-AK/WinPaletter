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
        private bool disposedValue; // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        // Protected Overrides Sub Finalize()
        // ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        // Dispose(False)
        // MyBase.Finalize()
        // End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Variables

        private List<Tuple<string, string, string, string>> _tree = [];

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
        /// <param name="File"></param>
        /// <param name="form"></param>
        public void Load(string File, System.Windows.Forms.Form form = null)
        {
            if (System.IO.File.Exists(File))
            {
                Program.Log?.Write(LogEventLevel.Information, $"Loading language from file `{File}`.");

                JObject JObj;

                using (StreamReader St = new(File))
                {
                    JObj = JObject.Parse(St.ReadToEnd());
                    St.Close();
                }

                Information = new();
                Strings = new();
                Forms = [];

                bool isValid = JObj.ContainsKey(nameof(Information)) && JObj.ContainsKey("Global Strings") && JObj.ContainsKey("Forms Strings");

                if (!isValid) return;

                Information = JObj[nameof(Information)].ToObject<Information_Cls>();
                Program.Log?.Write(LogEventLevel.Information, $"Information of language file have been loaded.");

                Strings = JObj["Global Strings"].ToObject<Strings_Cls>();
                Program.Log?.Write(LogEventLevel.Information, $"Global strings inside language file have been loaded.");

                Forms = (JObject)JObj["Forms Strings"];

                _tree = DeserializeFormsJSONIntoList(Forms);

                LoadFromStrings(form);
                Program.Log?.Write(LogEventLevel.Information, $"Forms strings inside language file have been loaded.");
            }
        }

        /// <summary>
        /// Save the language File as JSON format
        /// </summary>
        /// <param name="File"></param>
        /// <param name="Forms"></param>
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
                else
                {
                    j_Forms = [];
                }

                foreach (System.Windows.Forms.Form form in Forms)
                {
                    if (j_Forms.ContainsKey(form.Name))
                    {
                        j_Forms[form.Name] = form.ToJSON();
                    }
                    else
                    {
                        j_Forms.Add(form.Name, form.ToJSON());
                    }
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
        /// <param name="File"></param>
        /// <param name="formsJObject"></param>
        public void Save(string File, JObject formsJObject)
        {
            Information.AppVer = Program.Version;
            JObject JSON_Overall = [];

            JSON_Overall.Add(nameof(Information), JObject.FromObject(Information));
            JSON_Overall.Add("Global Strings", JObject.FromObject(Strings));
            JSON_Overall.Add("Forms Strings", formsJObject);

            System.IO.File.WriteAllText(File, JSON_Overall.ToString());
        }

        private List<Tuple<string, string, string, string>> DeserializeFormsJSONIntoList(JObject JSON_Forms)
        {
            // Tuple of four values; sub_form name, control name, property, property value
            // If there is no control and you want to change sub_form property, make control name: String.Empty
            List<Tuple<string, string, string, string>> tree = [];
            tree.Clear();

            string FormName, ControlName, Prop, Value;
            FormName = string.Empty;
            ControlName = string.Empty;
            Prop = string.Empty;
            Value = string.Empty;

            // Loop through all forms nodes in JObj
            foreach (KeyValuePair<string, JToken> F in JSON_Forms)
            {
                // Get one sub_form node
                // There is only one specific property "Text"
                JObject J_Specific_Form = [];
                J_Specific_Form = (JObject)JSON_Forms[F.Key];
                FormName = F.Key.ToString();
                ControlName = string.Empty;
                Prop = "Text";

                if (J_Specific_Form.ContainsKey("Text") | J_Specific_Form.ContainsKey("text") | J_Specific_Form.ContainsKey("TEXT"))
                {
                    if (J_Specific_Form.ContainsKey("Text")) Value = J_Specific_Form["Text"].ToString();
                    if (J_Specific_Form.ContainsKey("text")) Value = J_Specific_Form["text"].ToString();
                    if (J_Specific_Form.ContainsKey("TEXT")) Value = J_Specific_Form["TEXT"].ToString();
                    tree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                }

                // If this sub_form has a control/controls then get them
                if (J_Specific_Form.ContainsKey("Controls") | J_Specific_Form.ContainsKey("controls") | J_Specific_Form.ContainsKey("CONTROLS"))
                {
                    // JObj nodes of all child controls
                    JObject J_Controls = [];
                    if (J_Specific_Form.ContainsKey("Controls")) J_Controls = (JObject)J_Specific_Form["Controls"];
                    if (J_Specific_Form.ContainsKey("controls")) J_Controls = (JObject)J_Specific_Form["controls"];
                    if (J_Specific_Form.ContainsKey("CONTROLS")) J_Controls = (JObject)J_Specific_Form["CONTROLS"];

                    // Loop through all child controls JObj nodes
                    foreach (KeyValuePair<string, JToken> ctrl in J_Controls)
                    {
                        // If there is a dot in JObj node value, then there is a specific mentioned property,
                        // if not, then it is a "Text" property only.
                        if (ctrl.Key.Contains("."))
                        {
                            ControlName = ctrl.Key.Split('.')[0];
                            Prop = ctrl.Key.Split('.')[1] ?? "Text";
                            Value = ctrl.Value is not null ? ctrl.Value?.ToString() : string.Empty;
                            tree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                        }
                        else
                        {
                            ControlName = ctrl.Key.ToString();
                            Prop = "Text";
                            Value = ctrl.Value is not null ? ctrl.Value?.ToString() : string.Empty;
                            tree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                        }
                    }
                }
            }

            return tree;
        }

        private List<Tuple<string, string, string, string>> DeserializeOneFormJSONIntoList(string FormName, JObject JSON_Form)
        {
            if (JSON_Form is null) return null;

            // Tuple of four values; sub_form name, control name, property, property value
            // If there is no control and you want to change sub_form property, make control name: String.EmptyError
            List<Tuple<string, string, string, string>> tree = [];
            tree.Clear();

            string ControlName, Prop, Value;
            ControlName = string.Empty;
            Prop = string.Empty;
            Value = string.Empty;

            // Loop through all forms nodes in JObj
            foreach (KeyValuePair<string, JToken> F in JSON_Form)
            {
                ControlName = string.Empty;
                Prop = "Text";

                if (JSON_Form.ContainsKey("Text") | JSON_Form.ContainsKey("text") | JSON_Form.ContainsKey("TEXT"))
                {
                    if (JSON_Form.ContainsKey("Text")) Value = JSON_Form["Text"].ToString();
                    if (JSON_Form.ContainsKey("text")) Value = JSON_Form["text"].ToString();
                    if (JSON_Form.ContainsKey("TEXT")) Value = JSON_Form["TEXT"].ToString();
                    tree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                }

                // If this sub_form has a control/controls then get them
                if (JSON_Form.ContainsKey("Controls") | JSON_Form.ContainsKey("controls") | JSON_Form.ContainsKey("CONTROLS"))
                {
                    // JObj nodes of all child controls
                    JObject J_Controls = [];
                    if (JSON_Form.ContainsKey("Controls")) J_Controls = (JObject)JSON_Form["Controls"];
                    if (JSON_Form.ContainsKey("controls")) J_Controls = (JObject)JSON_Form["controls"];
                    if (JSON_Form.ContainsKey("CONTROLS")) J_Controls = (JObject)JSON_Form["CONTROLS"];

                    // Loop through all child controls JObj nodes
                    foreach (KeyValuePair<string, JToken> ctrl in J_Controls)
                    {
                        // If there is a dot in JObj node value, then there is a specific mentioned property,
                        // if not, then it is a "Text" property only.
                        if (ctrl.Key.Contains("."))
                        {
                            ControlName = ctrl.Key.Split('.')[0];
                            Prop = ctrl.Key.Split('.')[1] ?? "Text";
                            Value = ctrl.Value is not null ? ctrl.Value?.ToString() : string.Empty;
                            tree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                        }
                        else
                        {
                            ControlName = ctrl.Key.ToString();
                            Prop = "Text";
                            Value = ctrl.Value is not null ? ctrl.Value?.ToString() : string.Empty;
                            tree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                        }
                    }
                }
            }

            return tree;
        }

        /// <summary>
        /// Load the language strings into the forms
        /// </summary>
        /// <param name="form"></param>
        public void LoadFromStrings(System.Windows.Forms.Form form)
        {
            if (form is not null)
            {
                bool WasVisible = form.Visible;

                if (WasVisible) form.Visible = false;

                SetFormValues(_tree, form);

                if (WasVisible) form.Visible = true;
            }
        }

        /// <summary>
        /// Load the language strings into the forms
        /// </summary>
        /// <param name="form"></param>
        /// <param name="jObject"></param>
        public void LoadFromStrings(System.Windows.Forms.Form form, JObject jObject)
        {
            if (form is not null)
            {
                bool WasVisible = form.Visible;

                if (WasVisible) form.Visible = false;

                SetFormValues(DeserializeOneFormJSONIntoList(form.Name, jObject), form);

                if (WasVisible) form.Visible = true;
            }
        }

        /// <summary>
        /// Load the language of a form.
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="jObject"></param>
        /// <param name="Localizer"></param>
        public static void LoadLanguage(System.Windows.Forms.Form Form, JObject jObject, Localizer Localizer)
        {
            Localizer?.LoadFromStrings(Form, jObject);
        }

        /// <summary>
        /// Set the form values from the deserialized JSON tree
        /// </summary>
        /// <param name="PopCtrlList"></param>
        /// <param name="form"></param>
        private void SetFormValues(List<Tuple<string, string, string, string>> PopCtrlList, System.Windows.Forms.Form form)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Setting strings for form `{form.Name}`.");

            if (PopCtrlList is null) return;

            // Item1 = FormName
            // Item2 = ControlName
            // Item3 = Prop
            // Item4 = Value
            foreach (Tuple<string, string, string, string> member in PopCtrlList)
            {
                if ((form.Name.ToLower() ?? string.Empty) == (member.Item1.ToLower() ?? string.Empty))
                {
                    if (string.IsNullOrEmpty(member.Item2))
                    {
                        // # form
                        if (member.Item3 is not null && !string.IsNullOrWhiteSpace(member.Item3))
                        {
                            if (member.Item3.ToLower() == "text") form.SetText(member.Item4 ?? string.Empty);
                            else if (member.Item3.ToLower() == "tag") form.SetTag((member.Item4 ?? string.Empty).ToString());
                        }
                    }

                    // # Control
                    else if (!string.IsNullOrEmpty(member.Item2))
                    {
                        foreach (Control ctrl in form.Controls.Find(member.Item2, true))
                        {
                            if (member.Item3.ToLower() == "text")
                            {
                                ctrl.SetText(member.Item4.ToString());
                            }

                            if (member.Item3.ToLower() == "tag") ctrl.SetTag(member.Item4.ToString());
                        }
                    }
                }
            }
        }
    }
}