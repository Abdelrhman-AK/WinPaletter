using System;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// A form that allows the user to replace text in a DataGridView or a Form.
    /// </summary>
    public partial class Lang_ReplaceText
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Lang_ReplaceText"/> class.
        /// </summary>
        public Lang_ReplaceText()
        {
            InitializeComponent();
        }
        private void Lang_ReplaceText_Load(object sender, EventArgs e)
        {
            Icon = FormsExtensions.Icon<Lang_Editor>();
            this.LoadLanguage();
            ApplyStyle(this);

        }

        /// <summary>
        /// Replaces text in a DataGridView.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Column"></param>
        /// <param name="FindWhat"></param>
        /// <returns></returns>
        public string Replace(DataGridView data, int Column, string FindWhat)
        {
            // Hide the dialog
            using (Lang_ReplaceText dlg = new())
            {
                // Set the text to find
                dlg.TextBox3.Text = FindWhat;

                // If the user clicked OK, replace the text
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Get the search and replace text
                    string SearchText = dlg.TextBox3.Text;
                    string ReplaceBy = dlg.TextBox4.Text;

                    // If the search text is empty, return the replace text
                    if (string.IsNullOrWhiteSpace(SearchText)) return ReplaceBy;

                    // Replace the text in each cell in the specified column
                    for (int r = 0, loopTo = data.Rows.Count - 1; r <= loopTo; r++)
                    {
                        DataGridViewCell temp = data[Column, r];
                        temp.Value = temp.Value.ToString().Replace(SearchText, ReplaceBy, !dlg.CheckBox1.Checked, dlg.CheckBox2.Checked);
                    }

                    return ReplaceBy;
                }

                else
                {
                    return dlg.TextBox4.Text;
                }
            }
        }

        /// <summary>
        /// Replaces text in a Form.
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="FindWhat"></param>
        /// <returns></returns>
        public string Replace(Form Form, string FindWhat)
        {
            // Hide the dialog
            using (Lang_ReplaceText dlg = new())
            {
                // Set the text to find
                dlg.TextBox3.Text = FindWhat;

                // If the user clicked OK, replace the text
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Get the search and replace text
                    string SearchText = dlg.TextBox3.Text;
                    string ReplaceBy = dlg.TextBox4.Text;

                    // If the search text is empty, return the replace text
                    if (string.IsNullOrWhiteSpace(SearchText)) return ReplaceBy;

                    // Replace the text in each control
                    foreach (Control ctrl in Form.GetAllControls())
                    {
                        // If the control has text, replace the text
                        if (ctrl.Text is not null && !string.IsNullOrWhiteSpace(ctrl.Text))
                        {
                            ctrl.Text = ctrl.Text.Replace(SearchText, ReplaceBy, !dlg.CheckBox1.Checked, dlg.CheckBox2.Checked);
                        }

                        // If the control has a tag, replace the tag
                        else if (ctrl.Tag is not null && !string.IsNullOrWhiteSpace(ctrl.Tag.ToString()))
                        {
                            ctrl.Tag = ctrl.Tag.ToString().Replace(SearchText, ReplaceBy, !dlg.CheckBox1.Checked, dlg.CheckBox2.Checked);
                        }
                    }

                    // Return the replace text
                    return ReplaceBy;
                }

                else
                {
                    // Return the replace text
                    return dlg.TextBox4.Text;
                }
            }
        }



        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}