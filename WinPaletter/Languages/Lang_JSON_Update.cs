using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class Lang_JSON_Update
    {
        public Lang_JSON_Update()
        {
            InitializeComponent();
        }
        private void Lang_JSON_Update_Load(object sender, EventArgs e)
        {
            Icon = My.MyProject.Forms.Lang_JSON_Manage.Icon;
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (OpenJSONDlg.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = OpenJSONDlg.FileName;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (OpenJSONDlg.ShowDialog() == DialogResult.OK)
            {
                TextBox2.Text = OpenJSONDlg.FileName;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (SaveJSONDlg.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                var Lang = new Localizer();
                Lang.ExportJSON(SaveJSONDlg.FileName);
                Lang.Dispose();
                TextBox2.Text = SaveJSONDlg.FileName;
                Cursor = Cursors.Default;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            if (SaveJSONDlg.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                string _output = SaveJSONDlg.FileName;

                var _Old_File = new StreamReader(TextBox1.Text);
                JObject J_Old = JObject.Parse(_Old_File.ReadToEnd());
                _Old_File.Close();

                var _New_File = new StreamReader(TextBox2.Text);
                JObject J_New = JObject.Parse(_New_File.ReadToEnd());
                _New_File.Close();

                // Add information from the New File
                var J_Output = new JObject() { { "Information", J_New["Information"] } };

                // Manage Global Strings
                var J_GlobalStrings = new JObject();
                JObject x_old = (JObject)J_Old["Global Strings"];
                JObject x_new = (JObject)J_New["Global Strings"];
                foreach (var j in x_new.Properties())
                {
                    if (x_old[j.Name] is null)
                        J_GlobalStrings.Add(j.Name, j.Value);       // Add Missing Strings From New JObj
                }

                foreach (var j in x_old.Properties())
                {
                    if (CheckBox1.Checked)
                    {
                        if (x_new.ContainsKey(j.Name))
                            J_GlobalStrings.Add(j.Name, j.Value);  // Add with exclusion of Old JObj
                    }
                    else
                    {
                        J_GlobalStrings.Add(j.Name, j.Value);
                    }                                    // Add Rest of items from Old JObj
                }

                J_Output.Add("Global Strings", J_GlobalStrings);

                // Manage Forms
                var J_Forms = new JObject();
                x_old = (JObject)J_Old["Forms Strings"];
                x_new = (JObject)J_New["Forms Strings"];

                foreach (var j in x_new.Properties())
                {

                    if (x_old[j.Name] is null)
                    {
                        J_Forms.Add(j.Name, j.Value);                                         // Add Missing Forms From New JObj
                    }

                    else
                    {
                        JObject c_old = (JObject)x_old[j.Name]["Controls"];
                        JObject c_new = (JObject)x_new[j.Name]["Controls"];
                        var c = new JObject();

                        foreach (var jj in c_new.Properties())
                        {
                            if (c_old[jj.Name] is null)
                                c.Add(jj.Name, jj.Value);       // Add Missing Controls From New JObj
                        }

                        foreach (var jj in c_old.Properties())
                            c.Add(jj.Name, jj.Value);                                         // Add Rest of controls from Old JObj

                        x_new[j.Name]["Controls"] = c;
                        x_new[j.Name]["Text"] = x_old[j.Name]["Text"];

                    }

                }

                // Add Modification to the newly created JObj
                foreach (var j in x_new.Properties())
                {
                    if (!J_Forms.ContainsKey(j.Name))
                        J_Forms.Add(j.Name, j.Value);
                }

                J_Output.Add("Forms Strings", J_Forms);

                File.WriteAllText(_output, J_Output.ToString());

                Cursor = Cursors.Default;

                WPStyle.MsgBox(My.Env.Lang.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Language-creation-(old-methods)#3-update-your-language-file-when-a-new-winpaletter-is-released");
        }
    }
}