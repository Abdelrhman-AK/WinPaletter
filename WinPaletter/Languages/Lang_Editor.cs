using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Properties;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Simulation;

namespace WinPaletter
{
    /// <summary>
    /// The language editor form.
    /// </summary>
    public partial class Lang_Editor
    {

        #region Variables and events

        /// <summary>
        /// Event that is raised when a control is selected.
        /// </summary>
        public static event EventHandler ControlSelection;

        /// <summary>
        /// The currently selected control to be translated.
        /// </summary>
        private Control selectedControl;

        /// <summary>
        /// The list of forms and their translations JSON objects.
        /// </summary>
        private readonly Dictionary<string, JObject> FormsList = [];

        /// <summary>
        /// FileSystem to the language file.
        /// </summary>
        private string LangFile;

        /// <summary>
        /// The modified language localizer class instance.
        /// </summary>
        private Localizer modifiedLang = new();

        /// <summary>
        /// The native language localizer class instance.
        /// </summary>
        private readonly Localizer nativeLang = new();

        /// <summary>
        /// Boolean value indicating whether the tag is being edited.
        /// </summary>
        private bool EditingTag = true;

        /// <summary>
        /// Boolean value indicating whether an additional tag is being edited.
        /// </summary>
        private bool AdditionalTag = false;

        /// <summary>
        /// Boolean value indicating whether the user is allowed to edit the text.
        /// </summary>
        private bool AllowEditing = false;

        /// <summary>
        /// The form that is currently opened to be translated.
        /// </summary>
        private System.Windows.Forms.Form openedForm;

        /// <summary>
        /// The name of the currently selected form.
        /// </summary>
        private string selectedFormName;

        /// <summary>
        /// The search text used to highlight the search results.
        /// </summary>
        private string SearchText;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lang_Editor"/> class.
        /// </summary>
        public Lang_Editor()
        {
            InitializeComponent();
        }


        #endregion

        #region Helpers Voids/functions

        /// <summary>
        /// Open the language file to be translated.
        /// </summary>
        private void OpenFile()
        {
            // Create a new localizer instance and load from file
            modifiedLang = new();
            modifiedLang.Load(LangFile);

            Label9.Text = LangFile;

            // Load the language information
            TextBox3.Text = modifiedLang.Information.Lang;
            TextBox5.Text = modifiedLang.Information.Name;
            TextBox6.Text = modifiedLang.Information.TranslationVersion;
            TextBox7.Text = modifiedLang.Information.AppVer;
            RadioButton2.Checked = modifiedLang.Information.RightToLeft;

            // Load language code
            if (ComboBox2.Items.Contains(modifiedLang.Information.LangCode))
            {
                ComboBox2.SelectedItem = modifiedLang.Information.LangCode ?? "en-US";
            }
            else
            {
                ComboBox2.SelectedItem = "en-US";
            }

            // Load global strings; strings that are not associated with any form and are used globally inside code
            LoadGlobalStrings();

            // Load all forms into the list
            LoadAllMiniFormsIntoList();
        }

        /// <summary>
        /// Load the global strings from the language file. Global strings are strings that are not associated with any form and are used globally inside code.
        /// </summary>
        private void LoadGlobalStrings()
        {
            // Adjust the DataGridView style
            data.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            data.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            data.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            data.Columns[0].DefaultCellStyle.Font = Fonts.ConsoleMedium;
            data.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            data.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Load the global strings from the language file and also load from native localizer (Default English)
            JObject JObject_File = JToken.Parse(File.ReadAllText(LangFile))["Global Strings"] as JObject;
            JObject JObject_App = JObject.FromObject(nativeLang.Strings);

            // Populate loaded localizer's global string and also load native English (To make user see both in DataGridView)
            PopulateStrings(JObject_App, JObject_File, data);

            // Check if the entries are equal to the English entries, and highlight them if they are
            CheckIfEntriesEqualEnglish();
        }

        // Populate the DataGridView with the strings from the language file and the native localizer (Default English)
        private void PopulateStrings(JObject native, JObject fromFile, DataGridView dataGridView)
        {
            // Traverse the JObject recursively
            foreach (JProperty property in native.Properties())
            {
                // Get the path of the property
                string path = property.Name;

                // Check if the property value is another JObject, an array, or a simple value
                if (property.Value is JObject @object)
                {
                    // If the value is another JObject, recursively traverse it
                    PopulateStrings(@object, fromFile, dataGridView);
                }
                else if (property.Value is JArray array)
                {
                    // If the value is an array, process each item in the array
                    int index = 0;
                    foreach (JToken arrayItem in array)
                    {
                        // Check if the array item is another JObject, and recursively traverse it
                        if (arrayItem is JObject object1)
                        {
                            PopulateStrings(object1, fromFile, dataGridView);
                        }
                        else
                        {
                            // Add the path and value to DataGridView
                            dataGridView.Rows.Add($"{path}[{index}]", GetValueFromPath(fromFile, property.Value.Path), arrayItem);
                        }
                        index++;
                    }
                }
                else
                {
                    // Add the path and value to DataGridView
                    dataGridView.Rows.Add(property.Value.Path, GetValueFromPath(fromFile, property.Value.Path), property.Value);
                }
            }
        }

        // Get the value from the JObject using the path
        private string GetValueFromPath(JObject jObject, string path)
        {
            // Split the path by '.' to get the path parts
            string[] pathParts = path.Split('.');

            // Traverse the JObject using the path parts
            JObject current = jObject;

            for (int i = 0; i < pathParts.Length; i++)
            {
                if (current.ContainsKey(pathParts[i]))
                {
                    if (i == pathParts.Length - 1)
                    {
                        // Return the value at the last path part
                        return current[pathParts[i]].ToString();
                    }
                    else
                    {
                        // Move to the next nested object
                        current = current[pathParts[i]] as JObject;
                    }
                }
                else
                {
                    // Return an empty string if the path part is not found
                    return string.Empty;
                }
            }

            // Return an empty string if the path is empty
            return string.Empty;
        }

        /// <summary>
        /// Check if the entries are equal to the English entries, and highlight them if they are.
        /// </summary>
        private void CheckIfEntriesEqualEnglish()
        {
            for (int r = 0; r <= data.Rows.Count - 1; r++)
            {
                if ((data[1, r].Value ?? string.Empty).ToString().ToLower().Trim() == (data[2, r].Value ?? string.Empty).ToString().ToLower().Trim())
                {
                    // Highlight the row if the entries are equal
                    data[1, r].Style.BackColor = Program.Style.Schemes.Secondary.Colors.Back_Checked;
                }
                else
                {
                    // Remove the highlight if the entries are not equal
                    data[1, r].Style.BackColor = data[2, r].Style.BackColor;
                }
            }
        }

        // Load all mini forms into the list
        private void LoadAllMiniFormsIntoList()
        {
            // Clear the forms list and adjust controls visibility (Translation controls and properties)
            forms_box.Visible = true;
            properties_box.Visible = false;
            forms_box.Dock = DockStyle.Fill;
            properties_box.Dock = DockStyle.Fill;

            FormsList.Clear();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("WinPaletter Forms").ImageIndex = 0;

            // Load all forms into the list
            foreach (Type type in Forms.ITypes)
            {
                FormsList.Add(type.Name, modifiedLang.Forms[type.Name] as JObject);
                TreeNode n = treeView1.Nodes[0].Nodes.Add(type.Name);
                n.ImageIndex = 1;
                n.SelectedImageIndex = 1;
            }

            treeView1.ExpandAll();
        }

        // Create a mini form from the original form
        private System.Windows.Forms.Form CreateMiniForm(System.Windows.Forms.Form Form, System.Windows.Forms.Form OriginalForm)
        {
            // Create a new form with the same properties as the original form
            System.Windows.Forms.Form child = new()
            {
                Name = Form.Name,
                Text = Form.Text,
                Icon = Form.Icon,
                ControlBox = true,
                MinimizeBox = true,
                MaximizeBox = true,
                FormBorderStyle = FormBorderStyle.Sizable,
                Padding = Form.Padding,
                BackColor = Form.BackColor,
                ForeColor = Form.ForeColor,
                Font = Form.Font,
                Dock = Form.Dock,
                Size = Form.Size,
                Margin = Form.Margin,
                RightToLeft = Form.RightToLeft,
                RightToLeftLayout = Form.RightToLeftLayout,
                ShowIcon = Form.ShowIcon,
                ShowInTaskbar = false,
                TopLevel = false
            };

            // Adjust the size of the form if it has no border
            if (Form.FormBorderStyle == FormBorderStyle.None) child.Size += new Size(4 * 4 + 2, 24 * 2 - 6);

            // Populate the sub controls of the form
            PopulateSubControls(Form, child, OriginalForm);

            // Add event handlers to the form
            child.Load += Child_Load;
            child.Click += FormPageClicked;
            child.FormClosing += Child_Closing;
            child.FormClosed += Child_Closed;

            // Return the created form
            return child;
        }

        // Populate the sub controls of the form
        private void PopulateSubControls(Control From, Control To, System.Windows.Forms.Form OriginalForm)
        {
            foreach (Control ctrl in From.Controls)
            {
                // Check if the control has children and recursively populate them
                if (ctrl.HasChildren)
                {
                    // Check if the control is a TabControl and populate its tab pages
                    if (ctrl is TabControl control)
                    {
                        TabControl tabs = new()
                        {
                            Name = ctrl.Name,
                            Text = ctrl.Text,
                            Tag = (string)(ctrl.Tag ?? string.Empty),
                            Anchor = ctrl.Anchor,
                            BackColor = ctrl.BackColor,
                            ForeColor = Color.Black,
                            Size = ctrl.Size,
                            Location = ctrl.Location,
                            Dock = ctrl.Dock,
                            Font = ctrl.Font,
                            Alignment = control.Alignment
                        };

                        // Populate the tab pages of the tab control
                        for (int i = 0; i <= control.TabPages.Count - 1; i++)
                        {
                            TabPage TP = new()
                            {
                                Name = control.TabPages[i].Name,
                                Text = control.TabPages[i].Text,
                                Tag = (string)(control.TabPages[i].Tag ?? string.Empty),
                                BackColor = control.TabPages[i].BackColor,
                                ForeColor = control.TabPages[i].ForeColor,
                                Size = control.TabPages[i].Size,
                                Location = control.TabPages[i].Location,
                                Padding = control.TabPages[i].Padding,
                                Font = control.TabPages[i].Font,
                                AutoScroll = true
                            };

                            // Add event handlers to the tab page (A click on tab page will select it to be translated)
                            TP.Click += TabPageClicked;

                            // Populate the sub controls of the tab page
                            PopulateSubControls(control.TabPages[i], TP, OriginalForm);

                            // Add the tab page to the tab control
                            tabs.TabPages.Add(TP);
                        }

                        // Add the tab control to the form
                        tabs.Selected += TabControlSelected;

                        // Add the tab control to the form
                        To.Controls.Add(tabs);
                    }

                    else if (ctrl is Window)
                    {
                        // Create a new window simulation control
                        TextTranslationItem c = new()
                        {
                            Name = ctrl.Name,
                            Text = ctrl.Text,
                            Text_English = ctrl.Text,
                            Tag = ctrl.Tag ?? string.Empty,
                            Tag_English = (string)(ctrl.Tag ?? string.Empty),
                            Anchor = ctrl.Anchor,
                            Padding = ctrl.Padding,
                            Font = ctrl.Font,
                            Dock = ctrl.Dock,
                            Size = ctrl.Size,
                            Margin = ctrl.Margin,
                            RightToLeft = ctrl.RightToLeft,
                            Location = ctrl.Location,
                            TextAlign = ContentAlignment.TopLeft
                        };

                        // Add event handlers to the window simulation control (A focus of the control will select it to be translated)
                        c.GotFocus += TextControlSelected;

                        // Add the window simulation control to the form
                        To.Controls.Add(c);
                    }

                    else
                    {
                        // Create a new panel control (generic container)
                        Panel pnl = new()
                        {
                            Name = ctrl.Name,
                            Text = ctrl.Text,
                            Tag = (string)(ctrl.Tag ?? string.Empty),
                            BackColor = Color.FromArgb(10, OriginalForm.BackColor.Invert()),
                            ForeColor = ctrl.ForeColor,
                            Size = ctrl.Size,
                            Location = ctrl.Location,
                            Anchor = ctrl.Anchor,
                            Dock = ctrl.Dock,
                            Font = ctrl.Font,
                            BorderStyle = BorderStyle.None
                        };

                        // Populate the sub controls of the panel
                        PopulateSubControls(ctrl, pnl, OriginalForm);

                        // Add the panel to the form if it contains any controls and don't add empty panels
                        if (pnl.GetAllControls().OfType<TextTranslationItem>().Count() > 0)
                        {
                            To.Controls.Add(pnl);

                            // Add event handlers to the panel (A click on the panel will select it to be translated)
                            if (pnl.Parent is TabPage) pnl.MouseDown += ParentTabPageClicked;
                            else if (pnl.Parent is System.Windows.Forms.Form) pnl.Click += ParentFormPageClicked;
                        }
                        else
                        {
                            // Dispose the panel if it contains no controls
                            pnl?.Dispose();
                        }
                    }
                }
                else
                {
                    // Check if the control has text (not a number and not empty and has more than one character)
                    bool Condition0 = ctrl.Text is not null && !ctrl.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(ctrl.Text) && ctrl.Text.Count() > 1;

                    // A flag to check if the control has a tag and it's not a number and not empty and has more than one character
                    bool Condition1 = ctrl.Tag is not null && !ctrl.Tag.ToString().All(char.IsDigit) && !string.IsNullOrWhiteSpace(ctrl.Tag.ToString()) && ctrl.Tag.ToString().Count() > 1;

                    // A flag to check if the control is a text box or a separator or a numeric up down or a track bar
                    bool Condition2 = ctrl is not TextBox && ctrl is not UI.WP.TextBox && ctrl is not UI.WP.SeparatorH && ctrl is not UI.WP.SeparatorV && ctrl is not UI.WP.NumericUpDown && ctrl is not UI.WP.TrackBar;

                    // A flag to check if the control is a picture box and has an image
                    bool Condition3 = ctrl is PictureBox box && box.Image is not null;

                    // Check if the control meets the conditions to be translated
                    if ((Condition0 | Condition1) && Condition2)
                    {
                        TextTranslationItem c = new()
                        {
                            Name = ctrl.Name,
                            Text = ctrl.Text,
                            Text_English = !string.IsNullOrEmpty(ctrl.Name) ? OriginalForm.Controls.Find(ctrl.Name, true).FirstOrDefault()?.Text ?? string.Empty : string.Empty,
                            Tag = ctrl.Tag ?? string.Empty,
                            Tag_English = !string.IsNullOrEmpty(ctrl.Name) ? (OriginalForm.Controls.Find(ctrl.Name, true).FirstOrDefault()?.Tag as string ?? string.Empty) : string.Empty,
                            Anchor = ctrl.Anchor,
                            Padding = ctrl.Padding,
                            Font = ctrl.Font,
                            Dock = ctrl.Dock,
                            Size = ctrl.Size,
                            Margin = ctrl.Margin,
                            RightToLeft = ctrl.RightToLeft,
                            Location = ctrl.Location
                        };

                        if (ctrl is Label label)
                        {
                            c.TextAlign = label.TextAlign;
                            c.ImageAlign = label.ImageAlign;
                            c.TextImageRelation = TextImageRelation.ImageBeforeText;
                        }

                        else if (ctrl is UI.WP.Button button)
                        {
                            c.TextAlign = button.TextAlign;
                            c.ImageAlign = button.ImageAlign;
                            c.Image = !button.ImageGlyphEnabled ? button.Image : button.ImageGlyph;
                            c.TextImageRelation = button.TextImageRelation;
                        }

                        else if (ctrl is Button wf_button)
                        {
                            c.TextAlign = wf_button.TextAlign;
                            c.ImageAlign = wf_button.ImageAlign;
                            c.Image = wf_button.Image;
                            c.TextImageRelation = wf_button.TextImageRelation;
                        }

                        else if (ctrl is UI.WP.RadioImage radioImage)
                        {
                            c.TextAlign = radioImage.TextAlign;
                            c.ImageAlign = radioImage.ImageAlign;
                            c.Image = radioImage.Image;
                            c.TextImageRelation = radioImage.TextImageRelation;
                        }

                        else if (ctrl is UI.WP.CheckBox || ctrl is UI.WP.RadioButton)
                        {
                            c.TextAlign = ContentAlignment.MiddleLeft;
                        }

                        else if (ctrl is UI.WP.AlertBox)
                        {
                            c.Image = null;
                            c.TextAlign = ContentAlignment.MiddleLeft;
                        }

                        // Add event handlers to the text translation control (A focus of the control will select it to be translated)
                        c.GotFocus += TextControlSelected;

                        // Add the text translation control to the form
                        To.Controls.Add(c);
                    }

                    else if (Condition3)
                    {
                        // Create a new picture box control
                        PictureBox c = new()
                        {
                            Name = ctrl.Name,
                            Text = ctrl.Text,
                            Tag = (string)(ctrl.Tag ?? string.Empty),
                            Padding = ctrl.Padding,
                            Font = ctrl.Font,
                            Dock = ctrl.Dock,
                            Size = ctrl.Size,
                            Margin = ctrl.Margin,
                            RightToLeft = ctrl.RightToLeft,
                            Location = ctrl.Location,
                            Image = (ctrl as PictureBox).Image,
                            SizeMode = (ctrl as PictureBox).SizeMode
                        };

                        To.Controls.Add(c);
                    }
                }
            }
        }

        /// <summary>
        /// Create a JObject from the DataGridView.
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        private static JObject CreateJObjectFromDataGridView(DataGridView dataGridView)
        {
            // Create a new JObject
            JObject jObject = [];

            // Traverse the DataGridView rows and set the values in the JObject
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Skip the row if the values are null
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                    continue;

                string path = row.Cells[0].Value.ToString(); // FileSystem from the first column
                string value = row.Cells[1].Value.ToString(); // Value from the second column

                // Set the value in the JObject using the path
                SetJObjectValue(jObject, path, value);
            }

            // Return the created JObject
            return jObject;
        }

        /// <summary>
        /// Set a value in a JObject using a path and value.
        /// </summary>
        /// <param name="jObject"></param>
        /// <param name="path"></param>
        /// <param name="value"></param>
        private static void SetJObjectValue(JObject jObject, string path, string value)
        {
            // Split the path by '.' for nested objects and detect '[' for arrays
            string[] pathComponents = path.Split(['.'], StringSplitOptions.None);

            JToken currentToken = jObject;

            for (int i = 0; i < pathComponents.Length; i++)
            {
                string component = pathComponents[i];

                if (component.Contains("["))
                {
                    // Handle array indices
                    string propertyName = component.Substring(0, component.IndexOf("["));
                    int arrayIndex = int.Parse(component.Substring(component.IndexOf("[") + 1, component.IndexOf("]") - component.IndexOf("[") - 1));

                    if (currentToken[propertyName] == null || currentToken[propertyName] is not JArray)
                    {
                        currentToken[propertyName] = new JArray();
                    }

                    JArray array = (JArray)currentToken[propertyName];

                    // Ensure the array has enough elements
                    while (array.Count <= arrayIndex)
                    {
                        array.Add(new JObject());
                    }

                    // Move to the array element
                    currentToken = array[arrayIndex];
                }
                else
                {
                    // Handle normal properties
                    if (i == pathComponents.Length - 1)
                    {
                        // Set the value at the last component
                        ((JObject)currentToken)[component] = value;
                    }
                    else
                    {
                        // Create nested object if it does not exist
                        if (currentToken[component] == null || currentToken[component] is not JObject)
                        {
                            currentToken[component] = new JObject();
                        }

                        // Move to the nested object
                        currentToken = currentToken[component];
                    }
                }
            }
        }
        #endregion

        #region Child form events
        private void Child_Load(object sender, EventArgs e)
        {
            ApplyStyle((System.Windows.Forms.Form)sender);
        }

        private void Child_Closing(object sender, FormClosingEventArgs e)
        {
            // Save the language of the form when it's closing to be used in the built list
            JObject newLang = (sender as System.Windows.Forms.Form).ToJSON();
            FormsList[(sender as System.Windows.Forms.Form).Name] = newLang;
            forms_box.Visible = true;
            properties_box.Visible = false;
            openedForm.Hide();
            e.Cancel = true;
        }

        private void Child_Closed(object sender, FormClosedEventArgs e)
        {
            openedForm?.Dispose();
        }

        #endregion

        #region Language item control events

        /// <summary>
        /// Event that is raised when a control is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextControlSelected(object sender, EventArgs e)
        {
            // Set the pressed state of the previously selected control to false
            if (selectedControl is TextTranslationItem textTranslationItem)
            {
                // Set the pressed state of the control to false and invalidate it
                textTranslationItem.Pressed = false;
                textTranslationItem.Invalidate();
            }

            // Set the selected control to the sender control
            selectedControl = sender as Control;
            selectedControl.Focus();

            // Raise the control selection event
            ControlSelection?.Invoke(selectedControl, new EventArgs());
        }

        /// <summary>
        /// Event that is raised when a tab control is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControlSelected(object sender, TabControlEventArgs e)
        {
            // Set the pressed state of the previously selected control to false
            if (selectedControl is TextTranslationItem temp)
            {
                temp.Pressed = false;
                temp.Invalidate();
            }

            // Set the selected control to the tab page
            selectedControl = e.TabPage;
            selectedControl.Focus();
            ControlSelection?.Invoke(selectedControl, new EventArgs());
        }

        /// <summary>
        /// Event that is raised when a tab page is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabPageClicked(object sender, EventArgs e)
        {
            // Set the pressed state of the previously selected control to false
            if (selectedControl is TextTranslationItem item)
            {
                item.Pressed = false;
                item.Invalidate();
            }

            // Set the selected control to the tab page
            selectedControl = sender as Control;
            selectedControl.Focus();
            ControlSelection?.Invoke(selectedControl, new EventArgs());
        }

        /// <summary>
        /// Event that is raised when a parent tab page is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentTabPageClicked(object sender, EventArgs e)
        {
            // Set the pressed state of the previously selected control to false
            if (selectedControl is TextTranslationItem textTranslationItem)
            {
                textTranslationItem.Pressed = false;
                textTranslationItem.Invalidate();
            }

            // Set the selected control to the parent tab page
            selectedControl = (sender as Control).Parent as TabPage;
            selectedControl.Focus();
            ControlSelection?.Invoke(selectedControl, new EventArgs());
        }

        /// <summary>
        /// Event that is raised when a parent form page is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentFormPageClicked(object sender, EventArgs e)
        {
            // Set the pressed state of the previously selected control to false
            selectedControl = (sender as Control).Parent as System.Windows.Forms.Form;
            selectedControl.Focus();
            ControlSelection?.Invoke(selectedControl, new EventArgs());
        }

        /// <summary>
        /// Event that is raised when a form page is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPageClicked(object sender, EventArgs e)
        {
            // Set the pressed state of the previously selected control to false
            if (selectedControl is TextTranslationItem textTranslationItem)
            {
                textTranslationItem.Pressed = false;
                textTranslationItem.Invalidate();
            }

            selectedControl = sender as System.Windows.Forms.Form;
            ControlSelection.Invoke(selectedControl, new EventArgs());
        }

        /// <summary>
        /// Event that is raised when a control is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lang_JSON_GUI_ControlSelection(object sender, EventArgs e)
        {
            // Set this flag to prevent the text boxes from updating the control text
            AllowEditing = false;

            // Set the control name label in properties box to the selected control name
            ctrlName.Text = (sender as Control).Name;

            // Set the text boxes text in properties box to the selected control text and tag
            if (!string.IsNullOrWhiteSpace((sender as Control).Text) && (sender as Control).Tag is not null && !string.IsNullOrWhiteSpace((sender as Control).Tag.ToString()))
            {
                textbox_new.Text = (sender as Control).Text;
                textbox_additional.Text = (sender as Control).Tag.ToString();
                AdditionalTag = true;
                EditingTag = false;
            }

            // Set the text boxes text in properties box to the selected control text only
            else if (!string.IsNullOrWhiteSpace((sender as Control).Text))
            {
                textbox_new.Text = (sender as Control).Text;
                textbox_additional.Text = string.Empty;
                EditingTag = false;
                AdditionalTag = false;
            }

            // Set the text boxes text in properties box to the selected control tag only
            else if ((sender as Control).Tag is not null && !string.IsNullOrWhiteSpace((sender as Control).Tag.ToString()))
            {
                textbox_new.Text = (sender as Control).Tag.ToString();
                textbox_additional.Text = string.Empty;
                EditingTag = true;
                AdditionalTag = false;
            }

            // Set the text boxes text in properties box to empty
            else
            {
                textbox_new.Text = string.Empty;
                textbox_additional.Text = string.Empty;
                EditingTag = false;
                AdditionalTag = false;
            }

            if (sender is TextTranslationItem textTranslationItem)
            {
                // Set original English text in properties box
                if (!string.IsNullOrWhiteSpace(textTranslationItem.Text_English))
                {
                    textbox_english.Text = textTranslationItem.Text_English;
                    EditingTag = false;
                }

                // Set original English tag in properties box
                else if (!string.IsNullOrWhiteSpace(textTranslationItem.Tag_English))
                {
                    textbox_english.Text = textTranslationItem.Tag_English;
                    EditingTag = true;
                }

                // Set original English text and tag in properties box to empty
                else
                {
                    textbox_english.Text = string.Empty;
                    EditingTag = false;
                }
            }
            else if (sender is System.Windows.Forms.Form || sender is TabPage)
            {
                // Set original English text and tag in properties box to empty
                // There is no tag set for forms and tabpages in WinPaletter
                textbox_english.Text = string.Empty;
                EditingTag = false;
            }

            AllowEditing = true;
        }
        #endregion

        #region Search and replace

        /// <summary>
        /// Event that is raised when a cell is painted in the DataGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Check if the value is null or the search text is null or empty or the row index is less than 0
            if (e.Value is null || SearchText is null || string.IsNullOrWhiteSpace(SearchText) || e.RowIndex < 0)
                return;

            // Check if the column index is 1 and the checkbox is not checked
            if (e.ColumnIndex == 2 & !CheckBox1.Checked)
                return;

            // Check if the column index is 0 and the checkbox is not checked
            if (e.ColumnIndex == 0 & !CheckBox2.Checked)
                return;

            // Set the string format for the cell painting
            StringFormat sf = StringFormat.GenericTypographic;
            sf.FormatFlags = sf.FormatFlags | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.DisplayFormatControl;

            // Get the text value from the cell and the size of the text
            string text = e.Value.ToString();
            SizeF textSize = e.Graphics.MeasureString(text, Font, e.CellBounds.Width, sf);
            int keyPos = text.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase);

            // Check if the search text is found in the text value to draw the highlight
            if (keyPos >= 0)
            {
                // Paint the background of the cell
                e.PaintBackground(e.CellBounds, true);

                // Set the brush and the text metric size
                SolidBrush br = new(e.CellStyle.ForeColor);

                SizeF textMetricSize = new(0f, 0f);
                if (keyPos >= 1)
                {
                    string textMetric = text.Substring(0, keyPos);
                    textMetricSize = e.Graphics.MeasureString(textMetric, Font, e.CellBounds.Width, sf);
                }

                // Set the key size and the key rectangle
                SizeF keySize = e.Graphics.MeasureString(text.Substring(keyPos, SearchText.Length), Font, e.CellBounds.Width, sf);

                // Set the left position of the key rectangle
                float left = e.CellBounds.Left + (keyPos <= 0 ? 0f : textMetricSize.Width); // + 2

                // Set the key rectangle
                RectangleF keyRect = new(left, e.CellBounds.Top + 1, keySize.Width, keySize.Height);

                // Fill the key rectangle with the highlight color
                SolidBrush fillBrush = new(Program.Style.Schemes.Tertiary.Colors.Line_Hover((sender as DataGridView).Level()));
                e.Graphics.FillRectangle(fillBrush, keyRect);

                // Dispose the fill brush
                fillBrush.Dispose();

                // Draw the text in the cell
                e.Graphics.DrawString(text, Font, br, e.CellBounds, sf);

                e.Handled = true;

                // Dispose the brush
                br.Dispose();
            }
            else
            {
                // Do nothing, draw as usual
                return;
            }
        }

        /// <summary>
        /// Event that is raised when the search text is changed in the search text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            // Clear the selection in the DataGridView
            data.ClearSelection();

            // Check if the search text is null or empty or whitespace
            if (TextBox8.Text == null || string.IsNullOrWhiteSpace(TextBox8.Text))
            {
                data.Refresh();
                return;
            }

            // Set the search text to the lower case of the text in the search text box (to make the search case-insensitive)
            SearchText = (TextBox8.Text ?? string.Empty).ToLower().Trim();

            // Traverse the DataGridView rows and select the rows that contain the search text
            for (int r = 0; r <= data.Rows.Count - 1; r++)
            {
                // Check if the checkbox is checked and select the cell if the search text is found in the cell value
                data[1, r].Selected = (data[1, r].Value ?? string.Empty).ToString().ToLower().Trim().Contains(SearchText);

                // Check if the checkbox is checked and select the cell if the search text is found in the cell value
                if (CheckBox1.Checked)
                    data[2, r].Selected = (data[2, r].Value ?? string.Empty).ToString().ToLower().Trim().Contains(SearchText);

                // Check if the checkbox is checked and select the cell if the search text is found in the cell value
                if (CheckBox2.Checked)
                    data[0, r].Selected = (data[0, r].Value ?? string.Empty).ToString().ToLower().Trim().Contains(SearchText);
            }

            // Scroll to the first selected cell
            if (data.SelectedCells is not null && data.SelectedCells.Count > 0) { data.FirstDisplayedScrollingRowIndex = data.SelectedCells[0].RowIndex; }

            // Refresh the DataGridView
            data.Refresh();
        }

        /// <summary>
        /// Event that is raised when the replace button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button13_Click(object sender, EventArgs e)
        {
            TextBox9.Text = Forms.Lang_ReplaceText.Replace(openedForm, TextBox9.Text);
        }

        /// <summary>
        /// Event that is raised when the search text is changed in the search text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox9_TextChanged(object sender, EventArgs e)
        {
            foreach (TextTranslationItem ctrl in openedForm.GetAllControls().OfType<TextTranslationItem>())
            {
                ctrl.SearchHighlight = TextBox9.Text;
            }
        }
        #endregion

        /// <summary>
        /// Event that is raised when the form is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lang_JSON_GUI_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;         // Prevent exception error of cross-thread

            LangFile = string.Empty;
            AlertBox1.Visible = true;
            GroupBox8.Visible = false;
            TabControl1.Visible = false;

            ctrlName.Font = Fonts.ConsoleMedium;
            Label9.Font = ctrlName.Font;
            data.DoubleBuffer();

            // Set the event handlers
            ControlSelection += Lang_JSON_GUI_ControlSelection;

            // Set the image list
            imageList1.Images.Add("WinPaletter", Resources.LangNode_Main);
            imageList1.Images.Add("Form", AspectsResources.JSON);

            // Load culture names into the ComboBox
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            ComboBox2.Items.Clear();

            foreach (CultureInfo culture in cultures)
            {
                if (!ComboBox2.Items.Contains(culture.Name) & !string.IsNullOrWhiteSpace(culture.Name))
                    ComboBox2.Items.Add(culture.Name);
            }

            Refresh();
        }

        /// <summary>
        /// Event that is raised when the form is closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lang_JSON_GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Remove the event handler from translation control
            ControlSelection -= Lang_JSON_GUI_ControlSelection;

            // Dispose the opened form and the modified language
            modifiedLang?.Dispose();

            selectedControl = null;

            // Dispose the mini form for translation
            openedForm?.Dispose();
        }

        /// <summary>
        /// Event that is raised when list node is double clicked to open translation mini form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Check if the node has a parent and the text is not empty
            if (e.Node.Parent is not null && !string.IsNullOrWhiteSpace(e.Node.Text))
            {
                // Set the selected form name to the node text
                selectedFormName = e.Node.Text;

                // Check if the form name exists in the forms list
                if (FormsList.TryGetValue(selectedFormName, out JObject form_lang))
                {
                    // Create a new form instance from the selected form name
                    Form form = (Form)Activator.CreateInstance(Assembly.GetExecutingAssembly().GetTypes()
                                   .Where(t => t.IsSubclassOf(typeof(Form)) && !t.IsAbstract && t.Name == selectedFormName)
                                   .FirstOrDefault());

                    // Create a mini form from the original form
                    openedForm = CreateMiniForm(form, form);

                    // Load the language of the form
                    Localizer.LoadLanguage(openedForm, form_lang, modifiedLang);

                    // Set the form name label in properties box to the selected form name
                    openedForm.Show();

                    // Add the mini form to the split container panel as a child MDI form
                    SplitContainer1.Panel1.Controls.Add(openedForm);
                    forms_box.Visible = false;
                    properties_box.Visible = true;
                }
            }
        }

        /// <summary>
        /// Event that is raised when the translation text box text is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (AllowEditing && selectedControl is not null)
            {
                if (!EditingTag)
                {
                    selectedControl.Text = textbox_new.Text;
                }
                else
                {
                    selectedControl.Tag = textbox_new.Text;
                }
            }
        }

        /// <summary>
        /// Event that is raised when the additional text box text is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (AdditionalTag && AllowEditing && selectedControl is not null)
            {
                selectedControl.Tag = textbox_additional.Text;
            }
        }

        /// <summary>
        /// Event that is raised when the font button change is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button6_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new() { Font = textbox_new.Font })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textbox_new.Font = dlg.Font;
                    textbox_english.Font = dlg.Font;
                    textbox_additional.Font = dlg.Font;
                    data.Font = dlg.Font;
                }
            }
        }

        /// <summary>
        /// Event that is raised when the open button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button8_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.OpenJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    AlertBox1.Visible = false;
                    GroupBox8.Visible = true;
                    TabControl1.Visible = false;
                    Cursor = Cursors.WaitCursor;
                    LangFile = dlg.FileName;
                    OpenFile();
                    Cursor = Cursors.Default;
                    TabControl1.Visible = true;
                }
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textbox_new.RightToLeft = RadioButton2.Checked ? RightToLeft.Yes : RightToLeft.No;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            TextBox7.Text = Program.Version;
        }

        /// <summary>
        /// Event that is raised when the add snippet button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button9_Click(object sender, EventArgs e)
        {
            if (Forms.Lang_Add_Snippet.ShowDialog() == DialogResult.OK)
            {
                TextBox3.Text = Forms.Lang_Add_Snippet.result;
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            TextBox5.Text = Environment.UserName;
        }

        /// <summary>
        /// Event that is raised when 'export English and load' button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.SaveJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    AlertBox1.Visible = false;
                    TabControl1.Visible = false;
                    Cursor = Cursors.WaitCursor;
                    using (Localizer LangX = new())
                    {
                        LangX.Save(dlg.FileName);
                    }
                    LangFile = dlg.FileName;
                    OpenFile();
                    Cursor = Cursors.Default;
                    TabControl1.Visible = true;
                }
            }
        }

        /// <summary>
        /// Event that is raised when 'export English' button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.JSON, Title = Program.Localization.Strings.Extensions.SaveJSON })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    using (Localizer LangX = new())
                    {
                        LangX.Save(dlg.FileName);
                    }
                    Cursor = Cursors.Default;
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Event that is raised when the save button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(LangFile))
            {
                JObject formsJObject = JObject.FromObject(FormsList);

                modifiedLang.Save(LangFile, formsJObject);

                JObject JObj = JToken.Parse(File.ReadAllText(LangFile)) as JObject;

                // Create a new JObject for the information
                JObject j_info = new()
                {
                    { "Name".ToLower(), TextBox5.Text ?? Environment.UserName },
                    { "TranslationVersion".ToLower(), TextBox6.Text ?? "1.0" },
                    { "Lang".ToLower(), TextBox3.Text ?? "English" },
                    { "LangCode".ToLower(), ComboBox2.SelectedItem.ToString() ?? "EN-US" },
                    { "AppVer".ToLower(), TextBox7.Text ?? Program.Version },
                    { "RightToLeft".ToLower(), RadioButton2.Checked }
                };

                // Set the information JObject and the global strings JObject in the main JObject
                JObj["Information"] = j_info;
                JObj["Global Strings"] = CreateJObjectFromDataGridView(data);

                // Save the main JObject to the language file
                File.WriteAllText(LangFile, JObj.ToString());

                // Hide a message box that the language file is saved
                MsgBox(Program.Localization.Strings.Languages.Saved, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Event that is a cell has ended editing in the DataGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the cell value is null or empty
            if ((data[1, e.RowIndex].Value ?? string.Empty).ToString().ToLower().Trim() != (data[2, e.RowIndex].Value ?? string.Empty).ToString().ToLower().Trim() && !string.IsNullOrWhiteSpace((data[1, e.RowIndex].Value ?? string.Empty).ToString().ToLower().Trim()))
            {
                // Set the cell value to the modified language
                if ((data[1, e.RowIndex].Value ?? string.Empty).ToString().Contains("{") || (data[1, e.RowIndex].Value ?? string.Empty).ToString().Contains("{"))
                {
                    int count1 = (data[1, e.RowIndex].Value ?? string.Empty).ToString().Count(c => c == '{');
                    int count2 = (data[2, e.RowIndex].Value ?? string.Empty).ToString().Count(c => c == '{');
                    if (count1 != count2)
                    {
                        // Set a reddish color to the cell to indicate that the values are not equal
                        data[1, e.RowIndex].Style.BackColor = Program.Style.Schemes.Secondary.Colors.Back_Checked;
                    }
                    else
                    {
                        data[1, e.RowIndex].Style.BackColor = data[2, e.RowIndex].Style.BackColor;
                    }
                }
                else
                {
                    data[1, e.RowIndex].Style.BackColor = data[2, e.RowIndex].Style.BackColor;
                }
            }
            else
            {
                // Set a reddish color to the cell to indicate that the values are not equal
                data[1, e.RowIndex].Style.BackColor = Program.Style.Schemes.Secondary.Colors.Back_Checked;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            data.Refresh();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Wiki.LanguageCreation);
        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
        }

        private void Lang_JSON_GUI_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null && Parent is TabPage)
            {
                pin_button.Visible = false;
            }
            else
            {
                pin_button.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBox8.Text = Forms.Lang_ReplaceText.Replace(data, 1, TextBox8.Text);
        }
    }
}