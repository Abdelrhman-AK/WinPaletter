using System;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Lang_ReplaceText
    {
        public Lang_ReplaceText()
        {
            InitializeComponent();
        }
        private void Lang_ReplaceText_Load(object sender, EventArgs e)
        {
            Icon = My.MyProject.Forms.Lang_JSON_Manage.Icon;
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);

        }

        public string Replace(DataGridView data, int Column, string FindWhat)
        {
            using (var dlg = new Lang_ReplaceText())
            {
                dlg.TextBox3.Text = FindWhat;

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    string SearchText = dlg.TextBox3.Text;
                    string ReplaceBy = dlg.TextBox4.Text;

                    if (string.IsNullOrWhiteSpace(SearchText))
                        return ReplaceBy;

                    for (int r = 0, loopTo = data.Rows.Count - 1; r <= loopTo; r++)
                    {
                        {
                            var temp = data[Column, r];
                            temp.Value = temp.Value.ToString().Replace(SearchText, ReplaceBy, !dlg.CheckBox1.Checked, dlg.CheckBox2.Checked);
                        }
                    }

                    return ReplaceBy;
                }

                else
                {
                    return dlg.TextBox4.Text;
                }
            }
        }

        public string Replace(Form Form, string FindWhat)
        {
            using (var dlg = new Lang_ReplaceText())
            {
                dlg.TextBox3.Text = FindWhat;

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    string SearchText = dlg.TextBox3.Text;
                    string ReplaceBy = dlg.TextBox4.Text;

                    if (string.IsNullOrWhiteSpace(SearchText))
                        return ReplaceBy;

                    foreach (var ctrl in Form.GetAllControls())
                    {
                        if (ctrl.Text is not null && !string.IsNullOrWhiteSpace(ctrl.Text))
                        {
                            ctrl.Text = ctrl.Text.Replace(SearchText, ReplaceBy, !dlg.CheckBox1.Checked, dlg.CheckBox2.Checked);
                        }

                        else if (ctrl.Tag is not null && !string.IsNullOrWhiteSpace(ctrl.Tag.ToString()))
                        {
                            ctrl.Tag = ctrl.Tag.ToString().Replace(SearchText, ReplaceBy, !dlg.CheckBox1.Checked, dlg.CheckBox2.Checked);
                        }
                    }

                    return ReplaceBy;
                }

                else
                {
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