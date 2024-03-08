using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Saving_ex_list
    {
        public List<Tuple<string, Exception>> ex_List;
        public bool ApplyMode = true;

        public Saving_ex_list()
        {
            InitializeComponent();
        }

        private void ThemeApply_list_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            using (BugReport formIcon = new()) { Icon = formIcon.Icon; }

            TreeView1.ImageList = ImageLists.ThemeLog;
            TreeView1.Nodes.Clear();

            foreach (Tuple<string, Exception> x in ex_List)
            {
                TreeNode temp = TreeView1.Nodes.Add(x.Item1);
                temp.ImageKey = "error";
                temp.SelectedImageKey = "error";
            }

            alertBox1.Text = ApplyMode ? Program.Lang.ApplyingMode_ErrorDialog : Program.Lang.LoadingMode_ErrorDialog;

            ApplyMode = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode is not null)
                Forms.BugReport.ThrowError(ex_List[TreeView1.SelectedNode.Index].Item2);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}