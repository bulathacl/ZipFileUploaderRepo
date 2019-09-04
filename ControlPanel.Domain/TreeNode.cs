using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControlPanel.Domain
{
    public class TreeNode
    {
        public TreeNode(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TreeNode> Children { get; set; }
    }
}
