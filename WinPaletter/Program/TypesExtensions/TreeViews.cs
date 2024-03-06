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
        /// Serialize from a JSON file to TreeView Nodes
        /// </summary>
        public static void FromJSON(this TreeView TreeView, string JSON_File, string rootName)
        {
            StreamReader reader = new(JSON_File);
            JsonTextReader jsonReader = new(reader);
            JToken root = JToken.Load(jsonReader);
            reader.Close();

            TreeView.BeginUpdate();
            try
            {
                TreeView.Nodes.Clear();

                {
                    TreeNode temp = TreeView.Nodes.Add(rootName);
                    temp.ImageKey = "json";
                    temp.SelectedImageKey = "json";
                    temp.Tag = root;
                }

                AddNode(root, TreeView.Nodes[0]);

                TreeView.CollapseAll();
            }
            finally
            {
                TreeView.EndUpdate();
            }
        }

        private static void AddNode(JToken token, TreeNode inTreeNode)
        {
            if (token is null)
                return;

            if (token is JValue)
            {

                {
                    TreeNode temp = inTreeNode.Nodes.Add(token.ToString());
                    temp.ImageKey = "value";
                    temp.SelectedImageKey = "value";
                    temp.Tag = token;
                }
            }

            else if (token is JObject)
            {
                JObject obj = (JObject)token;

                foreach (JProperty property in obj.Properties())
                {
                    TreeNode childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(property.Name)).ToString()];
                    childNode.Tag = property;
                    AddNode(property.Value, childNode);
                }
            }

            else if (token is JArray)
            {
                JArray array = (JArray)token;

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
            JObject J_All = new();
            J_All.RemoveAll();

            foreach (TreeNode N in TreeNode.Nodes)
            {

                JObject J = new();
                J.RemoveAll();
                LoopThroughNodes(N, N, J);

                J_All.Add(N.Text, J);
            }

            return J_All.ToString();
        }

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
