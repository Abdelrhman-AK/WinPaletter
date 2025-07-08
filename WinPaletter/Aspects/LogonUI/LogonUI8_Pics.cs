using System;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class LogonUI8_Pics
    {
        public LogonUI8_Pics()
        {
            InitializeComponent();
        }
        private void LogonUI8_Pics_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            Icon = FormsExtensions.Icon<LogonUI>();

            if (Forms.LogonUI81.ID == 0)
                img0.Checked = true;
            if (Forms.LogonUI81.ID == 1)
                img1.Checked = true;
            if (Forms.LogonUI81.ID == 2)
                img2.Checked = true;
            if (Forms.LogonUI81.ID == 3)
                img3.Checked = true;
            if (Forms.LogonUI81.ID == 4)
                img4.Checked = true;
            if (Forms.LogonUI81.ID == 5)
                img5.Checked = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (img0.Checked)
                Forms.LogonUI81.ID = 0;
            if (img1.Checked)
                Forms.LogonUI81.ID = 1;
            if (img2.Checked)
                Forms.LogonUI81.ID = 2;
            if (img3.Checked)
                Forms.LogonUI81.ID = 3;
            if (img4.Checked)
                Forms.LogonUI81.ID = 4;
            if (img5.Checked)
                Forms.LogonUI81.ID = 5;
            Close();
        }
    }
}