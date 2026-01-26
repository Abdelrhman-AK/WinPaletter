using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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

        public static string ToJSON(this TreeView treeView)
        {
            JObject root = [];
            foreach (TreeNode node in treeView.Nodes)
            {
                var obj = ConvertNode(node);
                if (obj != null)
                {
                    foreach (var prop in obj.Properties())
                        root[prop.Name] = prop.Value;
                }
            }
            return JsonConvert.SerializeObject(root, Formatting.Indented);
        }

        private static JObject ConvertNode(TreeNode node)
        {
            // Leaf node -> return null, handled by parent
            if (node.Nodes.Count == 0) return null;

            JObject obj = [];

            if (node.Nodes.Count == 1 && node.Nodes[0].Nodes.Count == 0)
            {
                // Single leaf child -> key = parent, value = child text (split on colon)
                string key = node.Text;
                string value = node.Nodes[0].Text;

                int colonIndex = value.IndexOf(':');
                if (colonIndex > 0)
                {
                    value = value.Substring(colonIndex + 1).Trim();
                }

                obj[key] = string.IsNullOrEmpty(value) ? null : value;
            }
            else if (AllChildrenAreLeaves(node))
            {
                // Multiple leaf children -> parent object with key/value pairs
                JObject leafObj = [];
                foreach (TreeNode child in node.Nodes)
                {
                    string key = child.Text;
                    string value = child.Text;

                    int colonIndex = child.Text.IndexOf(':');
                    if (colonIndex > 0)
                    {
                        key = child.Text.Substring(0, colonIndex).Trim();
                        value = child.Text.Substring(colonIndex + 1).Trim();
                    }

                    leafObj[key] = string.IsNullOrEmpty(value) ? null : value;
                }
                obj[node.Text] = leafObj;
            }
            else
            {
                // Mixed or nested children
                JObject nestedObj = [];
                foreach (TreeNode child in node.Nodes)
                {
                    var childObj = ConvertNode(child);
                    if (childObj != null)
                    {
                        foreach (var prop in childObj.Properties())
                            nestedObj[prop.Name] = prop.Value;
                    }
                    else if (child.Nodes.Count == 0)
                    {
                        // Leaf with no colon -> just text value
                        nestedObj[child.Text] = null;
                    }
                }
                obj[node.Text] = nestedObj;
            }

            return obj;
        }

        private static bool AllChildrenAreLeaves(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes) if (child.Nodes.Count > 0) return false;
            return true;
        }
    }
}
