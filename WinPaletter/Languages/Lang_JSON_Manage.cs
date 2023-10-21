﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class Lang_JSON_Manage
    {
        public Lang_JSON_Manage()
        {
            InitializeComponent();
        }
        private void LangJSON_Manage_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);

            Label6.Font = Fonts.ConsoleMedium;
            TreeView1.ImageList = ImageLists.Language;

            if (Program.Settings.Language.Enabled & File.Exists(Program.Settings.Language.File))
            {
                TreeView1.FromJSON(Program.Settings.Language.File, Path.GetFileName(Program.Settings.Language.File));
                OpenJSONDlg.FileName = Program.Settings.Language.File;
                SaveJSONDlg.FileName = Program.Settings.Language.File;
            }
        }


        private void TreeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
                e.CancelEdit = true;
        }

        private void TreeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            TextBox1.Text = e.Label;
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Label6.Text = e.Node.FullPath;

            if (e.Node.Nodes.Count == 0)
            {
                Label4.Text = e.Node.Parent.Text;
                TextBox1.Text = e.Node.Text;
            }
            else if (e.Node.Nodes.Count == 1)
            {
                Label4.Text = e.Node.Text;
                TextBox1.Text = e.Node.Nodes[0].Text;
            }

            else
            {
                Label4.Text = "";
                TextBox1.Text = "";
            }

            TextBox3.Text = TextBox1.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode is not null)
            {
                if (TreeView1.SelectedNode.Nodes.Count == 0)
                {
                    TreeView1.SelectedNode.Text = TextBox1.Text;
                }

                else if (TreeView1.SelectedNode.Nodes.Count == 1)
                {
                    TreeView1.SelectedNode.Nodes[0].Text = TextBox1.Text;

                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {

            if (OpenJSONDlg.ShowDialog() == DialogResult.OK)
            {
                TreeView1.FromJSON(OpenJSONDlg.FileName, Path.GetFileName(OpenJSONDlg.FileName));
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {

            if (SaveJSONDlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveJSONDlg.FileName, TreeView1.Nodes[0].ToJSON());
            }

        }


        private void Button3_Click(object sender, EventArgs e)
        {
            string searchText = TextBox2.Text;

            if (string.IsNullOrEmpty(searchText))
            {
                return;
            }

            if ((LastSearchText ?? "") != (searchText ?? ""))
            {
                CurrentNodeMatches.Clear();
                LastSearchText = searchText;
                LastNodeIndex = 0;
                SearchNodes(searchText, TreeView1.Nodes[0]);
            }

            if (LastNodeIndex >= CurrentNodeMatches.Count)
                LastNodeIndex = 0;

            if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
            {
                var selectedNode = CurrentNodeMatches[LastNodeIndex];
                LastNodeIndex += 1;
                TreeView1.SelectedNode = selectedNode;
                TreeView1.SelectedNode.Expand();
                TreeView1.Select();
            }
        }


        private readonly List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;
        private string LastSearchText;


        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            while (StartNode is not null)
            {

                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                }

                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);
                }

                StartNode = StartNode.NextNode;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (SaveJSONDlg.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                var Lang = new Localizer();
                Lang.ExportJSON(SaveJSONDlg.FileName);
                Lang.Dispose();
                TreeView1.FromJSON(SaveJSONDlg.FileName, Path.GetFileName(SaveJSONDlg.FileName));
                Cursor = Cursors.Default;
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
                Cursor = Cursors.Default;
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Forms.Lang_Add_Snippet.ShowDialog();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            FontDialog1.Font = TextBox1.Font;

            if (FontDialog1.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Font = FontDialog1.Font;
                TextBox3.Font = FontDialog1.Font;
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button9_Click(object sender, EventArgs e)
        {

            if (Forms.Lang_Add_Snippet.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = Forms.Lang_Add_Snippet._Result;
            }

        }

        private void Button10_Click_1(object sender, EventArgs e)
        {
            TreeView1.Visible = false;
            TreeView1.ExpandAll();
            TreeView1.Visible = true;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            TreeView1.Visible = false;
            TreeView1.CollapseAll();
            TreeView1.Visible = true;
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Language-creation-(old-methods)");
        }
    }
}