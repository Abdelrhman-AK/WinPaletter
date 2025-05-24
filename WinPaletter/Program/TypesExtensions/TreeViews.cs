using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extensions for <see cref="TreeView"/>
    /// </summary>
    public static class TreeViewExtensions
    {
        /// <summary>
        /// Serialize from a JSON File to TreeView Nodes
        /// </summary>
        public static void FromJSON(this TreeView treeView, string JSON_File, string rootName)
        {
            StreamReader reader = new(JSON_File);
            JsonTextReader jsonReader = new(reader);
            JToken root = JToken.Load(jsonReader);
            reader.Close();

            treeView.BeginUpdate();
            try
            {
                treeView.Nodes.Clear();

                TreeNode temp = treeView.Nodes.Add(rootName);
                temp.ImageKey = "json";
                temp.SelectedImageKey = "json";
                temp.Tag = root;

                AddNode(root, treeView.Nodes[0]);

                treeView.CollapseAll();
            }
            finally
            {
                treeView.EndUpdate();
            }
        }

        /// <summary>
        /// Add a node to the TreeView from a JToken
        /// </summary>
        /// <param name="token"></param>
        /// <param name="inTreeNode"></param>
        private static void AddNode(JToken token, TreeNode inTreeNode)
        {
            if (token is null) return;

            if (token is JValue)
            {
                TreeNode temp = inTreeNode.Nodes.Add(token.ToString());
                temp.ImageKey = "value";
                temp.SelectedImageKey = "value";
                temp.Tag = token;
            }

            else if (token is JObject obj && obj is not null)
            {
                foreach (JProperty property in obj.Properties())
                {
                    if (property is not null)
                    {
                        TreeNode childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(property.Name)).ToString()];
                        childNode.Tag = property;
                        AddNode(property.Value, childNode);
                    }
                }
            }

            else if (token is JArray array && array is not null)
            {
                for (int i = 0, loopTo = array.Count - 1; i <= loopTo; i++)
                {
                    TreeNode childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(i.ToString()))];
                    childNode.Tag = array[i];
                    AddNode(array[i], childNode);
                }
            }
        }

        /// <summary>
        /// Serialize a node to JSON formatted String
        /// </summary>
        public static string ToJSON(this TreeNode TreeNode)
        {
            JObject J_All = [];
            J_All.RemoveAll();

            foreach (TreeNode N in TreeNode.Nodes)
            {

                JObject J = [];
                J.RemoveAll();
                LoopThroughNodes(N, N, J);

                J_All.Add(N.Text, J);
            }

            return J_All.ToString();
        }

        /// <summary>
        /// Loop through the nodes and add them to a JObject
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="MainNode"></param>
        /// <param name="JSON"></param>
        private static void LoopThroughNodes(TreeNode Node, TreeNode MainNode, JObject JSON)
        {
            foreach (TreeNode N in Node.Nodes)
            {
                if (N.Nodes.Count == 1)
                {
                    JSON.Add(N.Text, N.Nodes[0].Text);
                }
                else if (N.Nodes.Count > 1)
                {
                    JSON.Add(N.Text, new JObject());
                    JObject Jx = (JObject)JSON[N.Text];
                    LoopThroughNodes(N, MainNode, Jx);
                }
            }
        }
    }
}
