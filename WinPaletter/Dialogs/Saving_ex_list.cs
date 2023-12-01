using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Saving_ex_list
    {

        public List<Tuple<string, Exception>> ex_List;

        public Saving_ex_list()
        {
            InitializeComponent();
        }

        private void ThemeApply_list_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = Forms.BugReport.Icon;

            TreeView1.ImageList = ImageLists.ThemeLog;
            TreeView1.Nodes.Clear();

            foreach (Tuple<string, Exception> x in ex_List)
            {
                {
                    TreeNode temp = TreeView1.Nodes.Add(x.Item1);
                    temp.ImageKey = "error";
                    temp.SelectedImageKey = "error";
                }
            }
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