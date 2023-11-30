using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public JObject JObj;
        private JObject J_Information;
        private JObject J_GlobalStrings;
        private JObject J_Forms;
        private List<Tuple<string, string, string, string>> Deserialized_FormsJSONTree = new List<Tuple<string, string, string, string>>();
        #endregion

        public Localizer() { }

        public void Load(string File, Form _Form = null)
        {
            if (System.IO.File.Exists(File))
            {

                using (var St = new StreamReader(File))
                {
                    JObj = JObject.Parse(St.ReadToEnd());
                    St.Close();
                }

                J_Information= new();
                J_GlobalStrings= new();
                J_Forms= new();

                bool Valid = JObj.ContainsKey("Information") & JObj.ContainsKey("Global Strings") & JObj.ContainsKey("Forms Strings");

                if (!Valid)
                {
                    // $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

                    return;
                }

                J_Information = (JObject)JObj["Information"];
                J_GlobalStrings = (JObject)JObj["Global Strings"];
                J_Forms = (JObject)JObj["Forms Strings"];

                LoadInnerStrings(J_Information, J_GlobalStrings);
                DeserializeFormsJSONIntoTreeList(J_Forms);
                LoadFromStrings(_Form);

            }
        }

        public void LoadInnerStrings(JObject LangInfo, JObject Strings)
        {
            var type1 = GetType();
            PropertyInfo[] properties1 = type1.GetProperties();

            foreach (PropertyInfo property in properties1)
            {
                if (!((property.Name.ToLower() ?? string.Empty) == ("Name".ToLower() ?? string.Empty)) && !((property.Name.ToLower() ?? string.Empty) == ("TranslationVersion".ToLower() ?? string.Empty)) && !((property.Name.ToLower() ?? string.Empty) == ("Lang".ToLower() ?? string.Empty)) && !((property.Name.ToLower() ?? string.Empty) == ("LangCode".ToLower() ?? string.Empty)) && !((property.Name.ToLower() ?? string.Empty) == ("AppVer".ToLower() ?? string.Empty)) && !((property.Name.ToLower() ?? string.Empty) == ("RightToLeft".ToLower() ?? string.Empty)))
                {
                    if (Strings.ContainsKey(property.Name.ToLower()))
                        property.SetValue(this, Convert.ChangeType(Strings[property.Name.ToLower()], property.PropertyType));
                }
                else if (LangInfo.ContainsKey(property.Name.ToLower()))
                    property.SetValue(this, Convert.ChangeType(LangInfo[property.Name.ToLower()], property.PropertyType));
            }
        }

        public void DeserializeFormsJSONIntoTreeList(JObject JSON_Forms)
        {

            // Tuple of four values; form name, control name, property, property value
            // If there is no control and you want to change form property, make control name: String.Empty
            Deserialized_FormsJSONTree.Clear();

            string FormName, ControlName, Prop, Value;
            FormName = string.Empty;
            ControlName = string.Empty;
            Prop = string.Empty;
            Value = string.Empty;

            // Loop through all forms nodes in JObj
            foreach (var F in JSON_Forms)
            {
                try
                {

                    // Get one form node
                    // There is only one specific property "Text"
                    JObject J_Specific_Form = new();
                    J_Specific_Form = (JObject)JSON_Forms[F.Key];
                    FormName = F.Key.ToString();
                    ControlName = string.Empty;
                    Prop = "Text";

                    if (J_Specific_Form.ContainsKey("Text") | J_Specific_Form.ContainsKey("text") | J_Specific_Form.ContainsKey("TEXT"))
                    {
                        if (J_Specific_Form.ContainsKey("Text"))
                            Value = J_Specific_Form["Text"].ToString();
                        if (J_Specific_Form.ContainsKey("text"))
                            Value = J_Specific_Form["text"].ToString();
                        if (J_Specific_Form.ContainsKey("TEXT"))
                            Value = J_Specific_Form["TEXT"].ToString();
                        Deserialized_FormsJSONTree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                    }

                    // If this form has a control/controls then get them
                    if (J_Specific_Form.ContainsKey("Controls") | J_Specific_Form.ContainsKey("controls") | J_Specific_Form.ContainsKey("CONTROLS"))
                    {

                        // JObj nodes of all child controls
                        JObject J_Controls = new();
                        if (J_Specific_Form.ContainsKey("Controls"))
                            J_Controls = (JObject)J_Specific_Form["Controls"];
                        if (J_Specific_Form.ContainsKey("controls"))
                            J_Controls = (JObject)J_Specific_Form["controls"];
                        if (J_Specific_Form.ContainsKey("CONTROLS"))
                            J_Controls = (JObject)J_Specific_Form["CONTROLS"];

                        // Loop through all child controls JObj nodes
                        foreach (var ctrl in J_Controls)
                        {
                            try
                            {
                                // If there is a dot in JObj node value, then there is a specific mentioned property,
                                // if not, then it is a "Text" property only.
                                if (Conversions.ToBoolean(ctrl.Key.Contains(".")))
                                {
                                    ControlName = ctrl.Key.Split('.')[0];
                                    Prop = ctrl.Key.Split('.')[1];
                                    Value = ctrl.Value.ToString();
                                    Deserialized_FormsJSONTree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                                }
                                else
                                {
                                    ControlName = ctrl.Key.ToString();
                                    Prop = "Text";
                                    Value = ctrl.Value.ToString();
                                    Deserialized_FormsJSONTree.Add(new Tuple<string, string, string, string>(FormName, ControlName, Prop, Value));
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                catch
                {
                }
            }

        }

        public void LoadFromStrings(Form _Form = null)
        {
            if (_Form is not null)
            {
                bool WasVisible = _Form.Visible;

                if (WasVisible)
                {
                    _Form.Visible = false;
                }

                Populate(Deserialized_FormsJSONTree, _Form);

                if (WasVisible)
                    _Form.Visible = true;
            }
        }

        public void Populate(List<Tuple<string, string, string, string>> PopCtrlList, Form Form)
        {
            // Item1 = FormName
            // Item2 = ControlName
            // Item3 = Prop
            // Item4 = Value

            foreach (var member in PopCtrlList)
            {
                try
                {
                    if ((Form.Name.ToLower() ?? string.Empty) == (member.Item1.ToLower() ?? string.Empty))
                    {

                        if (string.IsNullOrEmpty(member.Item2))
                        {
                            // # Form
                            try
                            {
                                if (member.Item3.ToLower() == "text")
                                    Form.SetText(member.Item4);
                            }
                            catch
                            {
                            }

                            try
                            {
                                if (member.Item3.ToLower() == "tag")
                                    Form.SetTag(member.Item4.ToString());
                            }
                            catch
                            {
                            }
                        }
                        // # Control
                        else if (!string.IsNullOrEmpty(member.Item2))
                        {

                            foreach (Control ctrl in Form.Controls.Find(member.Item2, true))
                            {

                                try
                                {
                                    if (member.Item3.ToLower() == "text")
                                    {
                                        if ((member.Item1.ToLower() ?? string.Empty) != (Forms.Whatsnew.Name.ToLower() ?? string.Empty))
                                        {
                                            ctrl.SetText(member.Item4.ToString());
                                        }
                                        else if (!Forms.Whatsnew.TabControl1.TabPages.Cast<TabPage>().SelectMany(tp => tp.Controls.OfType<Control>()).Contains(ctrl) & !(ctrl is TabPage))
                                        {
                                            ctrl.SetText(member.Item4.ToString());
                                        }
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (member.Item3.ToLower() == "tag")
                                        ctrl.SetTag(member.Item4.ToString());
                                }
                                catch
                                {
                                }

                            }


                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public void ExportJSON(string File, Form[] Forms = null)
        {
            JObject JSON_Overall= new();
            Localizer newL = new();

            JObject j_info = new();
            j_info.RemoveAll();
            j_info.Add("Name".ToLower(), newL.Name);
            j_info.Add("TranslationVersion".ToLower(), newL.TranslationVersion);
            j_info.Add("Lang".ToLower(), newL.Lang);
            j_info.Add("LangCode".ToLower(), newL.LangCode);
            j_info.Add("AppVer".ToLower(), Program.Version);
            j_info.Add("RightToLeft".ToLower(), newL.RightToLeft);

            JObject j_globalstrings = new();

            var type1 = newL.GetType();
            PropertyInfo[] properties1 = type1.GetProperties();

            foreach (PropertyInfo property in properties1)
            {
                if (!string.IsNullOrWhiteSpace(property.GetValue(newL).ToString()) & !((property.Name.ToLower() ?? string.Empty) == ("Name".ToLower() ?? string.Empty)) & !((property.Name.ToLower() ?? string.Empty) == ("TranslationVersion".ToLower() ?? string.Empty)) & !((property.Name.ToLower() ?? string.Empty) == ("Lang".ToLower() ?? string.Empty)) & !((property.Name.ToLower() ?? string.Empty) == ("LangCode".ToLower() ?? string.Empty)) & !((property.Name.ToLower() ?? string.Empty) == ("AppVer".ToLower() ?? string.Empty)) & !((property.Name.ToLower() ?? string.Empty) == ("RightToLeft".ToLower() ?? string.Empty)))
                {
                    j_globalstrings.Add(property.Name.ToLower(), property.GetValue(newL).ToString());
                }
            }

            JObject j_Forms = new();

            if (Forms is null)
            {
                foreach (var f in Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(Form).IsAssignableFrom(t)))
                {
                    var ins = new Form();
                    ins = (Form)Activator.CreateInstance(f);

                    if ((ins.Name.ToLower() ?? string.Empty) != (WinPaletter.Forms.BK.Name.ToLower() ?? string.Empty))
                    {
                        JObject j_ctrl= new(), j_child= new();
                        j_ctrl.RemoveAll();
                        j_child.RemoveAll();

                        j_ctrl.Add("Text", ins.Text);

                        foreach (var ctrl in ins.GetAllControls())
                        {

                            if (!string.IsNullOrWhiteSpace(ctrl.Text) && !ctrl.Text.All(char.IsDigit) && !(ctrl.Text.Count() == 1) && !((ctrl.Text ?? string.Empty) == (ctrl.Name ?? string.Empty)))
                            {

                                if ((ins.Name.ToLower() ?? string.Empty) != (WinPaletter.Forms.Whatsnew.Name.ToLower() ?? string.Empty))
                                {
                                    j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                }
                                else
                                {
                                    try
                                    {
                                        if (!ins.Controls.OfType<UI.WP.TabControl>().ElementAt(0).TabPages.Cast<TabPage>().SelectMany(tp => tp.Controls.OfType<Control>()).Contains(ctrl) & !(ctrl is TabPage))
                                        {
                                            j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                        }
                                    }
                                    catch
                                    {
                                        j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                    }
                                }

                            }

                            if (ctrl.Tag is not null && !string.IsNullOrWhiteSpace(ctrl.Tag.ToString()) && !ctrl.Tag.ToString().All(char.IsDigit))
                            {
                                j_child.Add(ctrl.Name + ".Tag", ctrl.Tag.ToString());
                            }

                        }

                        if (j_ctrl.Count != 0)
                            j_ctrl.Add("Controls", j_child);

                        j_Forms.Add(ins.Name, j_ctrl);
                    }

                    ins.Dispose();
                }
            }
            else
            {
                bool Overwrite = System.IO.File.Exists(File);

                if (Overwrite)
                {
                    JObject OldSource = (JObject)JToken.Parse(System.IO.File.ReadAllText(File));
                    j_Forms = (JObject)OldSource["Forms Strings"];
                }

                foreach (var f in Forms)
                {
                    if ((f.Name.ToLower() ?? string.Empty) != (WinPaletter.Forms.BK.Name.ToLower() ?? string.Empty))
                    {
                        JObject j_ctrl= new(), j_child= new();
                        j_ctrl.RemoveAll();
                        j_child.RemoveAll();

                        j_ctrl.Add("Text", f.Text);

                        foreach (var ctrl in f.GetAllControls())
                        {

                            if (!string.IsNullOrWhiteSpace(ctrl.Text) && !ctrl.Text.All(char.IsDigit) && !(ctrl.Text.Count() == 1) && !((ctrl.Text ?? string.Empty) == (ctrl.Name ?? string.Empty)))
                            {

                                if ((f.Name.ToLower() ?? string.Empty) != (WinPaletter.Forms.Whatsnew.Name.ToLower() ?? string.Empty))
                                {
                                    j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                }
                                else
                                {
                                    try
                                    {
                                        if (!f.Controls.OfType<UI.WP.TabControl>().ElementAt(0).TabPages.Cast<TabPage>().SelectMany(tp => tp.Controls.OfType<Control>()).Contains(ctrl) & !(ctrl is TabPage))
                                        {
                                            j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                        }
                                    }
                                    catch
                                    {
                                        j_child.Add(ctrl.Name + ".Text", ctrl.Text);
                                    }
                                }

                            }

                            if (!string.IsNullOrWhiteSpace(ctrl.Tag.ToString()))
                            {
                                j_child.Add(ctrl.Name + ".Tag", ctrl.Tag.ToString());
                            }

                        }

                        if (j_ctrl.Count != 0)
                            j_ctrl.Add("Controls", j_child);

                        if (Overwrite)
                        {
                            j_Forms[f.Name] = j_ctrl;
                        }
                        else
                        {
                            j_Forms.Add(f.Name, j_ctrl);
                        }

                    }
                }
            }

            JSON_Overall.Add("Information", j_info);
            JSON_Overall.Add("Global Strings", j_globalstrings);
            JSON_Overall.Add("Forms Strings", j_Forms);

            System.IO.File.WriteAllText(File, JSON_Overall.ToString());
        }
    }

    public static class FormLangHelper
    {

        public static void LoadLanguage(this Form Form, Localizer Localizer = null)
        {
            if (Localizer is null)
            {
                if (Program.Settings.Language.Enabled && File.Exists(Program.Settings.Language.File))
                    Program.Lang.LoadFromStrings(Form);
            }
            else
            {
                Localizer.LoadFromStrings(Form);
            }
        }

    }
}