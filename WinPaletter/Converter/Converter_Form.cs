using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class Converter_Form
    {

        private readonly Converter _Convert = new Converter();

        public Converter_Form()
        {
            InitializeComponent();
        }

        private void Converter_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            CheckBox1.Checked = My.Env.Settings.FileTypeManagement.CompressThemeFile;
            Label3.Font = My.MyProject.Application.ConsoleFontMedium;

            if (System.IO.File.Exists(My.MyProject.Forms.MainFrm.OpenFileDialog1.FileName) && !System.IO.File.Exists(TextBox1.Text))
            {
                TextBox1.Text = My.MyProject.Forms.MainFrm.OpenFileDialog1.FileName;
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            switch (_Convert.FetchFile(TextBox1.Text))
            {
                case Converter_CP.WP_Format.JSON:
                    {
                        Label3.Text = My.Env.Lang.Convert_JSON_To_Old;
                        CheckBox2.Enabled = true;
                        CheckBox1.Enabled = false;
                        break;
                    }

                case Converter_CP.WP_Format.WPTH:
                    {
                        Label3.Text = My.Env.Lang.Convert_Old_To_JSON;
                        CheckBox2.Enabled = false;
                        CheckBox1.Enabled = true;
                        break;
                    }

                case Converter_CP.WP_Format.Error:
                    {
                        Label3.Text = My.Env.Lang.Convert_Error_Phrasing;
                        CheckBox2.Enabled = false;
                        CheckBox1.Enabled = false;
                        break;
                    }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (!(_Convert.FetchFile(TextBox1.Text) == Converter_CP.WP_Format.Error))
            {
                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _Convert.Convert(TextBox1.Text, SaveFileDialog1.FileName, CheckBox1.Checked, CheckBox2.Checked);
                    WPStyle.MsgBox(My.Env.Lang.Convert_Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = OpenFileDialog1.FileName;
            }
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Convert-WinPaletter-themes-between-old-and-new-formats");
        }
    }
}