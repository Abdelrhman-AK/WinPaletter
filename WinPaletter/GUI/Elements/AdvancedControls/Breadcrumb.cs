using System;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    public partial class BreadcrumbControl : UserControl
    {
        private FlowLayoutPanel breadcrumbPanel;
        private TextBox pathTextBox;

        private TreeView boundTreeView;
        public TreeView BoundTreeView
        {
            get => boundTreeView;
            set
            {
                if (boundTreeView != null)
                    boundTreeView.AfterSelect -= TreeView_AfterSelect;

                boundTreeView = value;

                if (boundTreeView != null)
                    boundTreeView.AfterSelect += TreeView_AfterSelect;

                UpdateBreadcrumb(boundTreeView?.SelectedNode);
            }
        }

        public BreadcrumbControl()
        {
            InitializeControls();
        }

        private void InitializeControls()
        {
            // FlowLayoutPanel
            breadcrumbPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = false
            };
            breadcrumbPanel.Click += BreadcrumbPanel_Click;
            this.Controls.Add(breadcrumbPanel);

            // TextBox
            pathTextBox = new TextBox
            {
                Visible = false,
                Dock = DockStyle.Fill
            };
            pathTextBox.KeyDown += PathTextBox_KeyDown;
            pathTextBox.LostFocus += PathTextBox_LostFocus;
            this.Controls.Add(pathTextBox);
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateBreadcrumb(e.Node);
        }

        public void UpdateBreadcrumb(TreeNode node)
        {
            breadcrumbPanel.Controls.Clear();

            if (node == null) return;

            var pathNodes = node.FullPath.Split('\\');

            TreeNode current = boundTreeView.TopNode;
            for (int i = 0; i < pathNodes.Length; i++)
            {
                string segment = pathNodes[i];
                TreeNode matching = current.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == segment) ?? current;

                Button btn = new()
                {
                    Text = segment,
                    AutoSize = true,
                    Tag = matching,
                    Flag = Button.Flags.TintedOnHover,
                    ImageGlyphEnabled = true,
                    CustomColor = Program.Style.Schemes.Main.Colors.Accent
                };

                btn.Width = (int)btn.Text.Measure(btn.Font).Width + 5;
                btn.Click += Btn_Click;
                breadcrumbPanel.Controls.Add(btn);

                // Add separator only if it's NOT the last segment
                if (i < pathNodes.Length - 1 && matching.Nodes.Count > 0)
                {
                    AlertBox sep = new()
                    {
                        Text = "▶",
                        AutoSize = true,
                        CenterText = true,
                        AlertStyle = AlertBox.Style.Simple,
                        Size = new(16, btn.Height),
                        Font = Fonts.ConsoleMedium
                    };
                    breadcrumbPanel.Controls.Add(sep);
                }

                current = matching;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is TreeNode node)
            {
                boundTreeView.SelectedNode = node;
            }
        }

        private void BreadcrumbPanel_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs me)
            {
                if (breadcrumbPanel.GetChildAtPoint(me.Location) == null)
                {
                    pathTextBox.Text = string.Join("\\", GetCurrentPathNodes());
                    pathTextBox.Visible = true;
                    pathTextBox.BringToFront();
                    pathTextBox.Focus();
                    pathTextBox.SelectedText = pathTextBox.Text;
                }
            }
        }

        private void PathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NavigateToPath(pathTextBox.Text);
                pathTextBox.Visible = false;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                pathTextBox.Visible = false;
            }
        }

        private void PathTextBox_LostFocus(object sender, EventArgs e)
        {
            pathTextBox.Visible = false;
        }

        private void NavigateToPath(string path)
        {
            if (boundTreeView == null || boundTreeView.TopNode == null) return;

            TreeNode node = FindNodeByPath(path.Split('\\'), boundTreeView.TopNode);
            if (node != null)
            {
                boundTreeView.SelectedNode = node;
            }
            else
            {
                MessageBox.Show("Path not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private TreeNode FindNodeByPath(string[] segments, TreeNode current)
        {
            if (current == null || segments.Length == 0) return null;

            if (current.Text != segments[0]) return null;

            if (segments.Length == 1) return current;

            var next = current.Nodes.Cast<TreeNode>()
                .FirstOrDefault(n => n.Text == segments[1]);

            return FindNodeByPath(segments.Skip(1).ToArray(), next);
        }

        private string[] GetCurrentPathNodes()
        {
            if (boundTreeView?.SelectedNode == null)
                return new string[0];

            return boundTreeView.SelectedNode.FullPath.Split('\\');
        }
    }
}
