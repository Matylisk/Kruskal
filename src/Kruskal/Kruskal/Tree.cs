using System.Collections.Generic;

namespace Kruskal
{
    public class Tree
    {
        public Tree(Node initialNode)
        {
            Nodes = new List<Node>();
            AddNode(initialNode);
            HeadLabel = initialNode.Label;
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
            node.AssignedTree = this;
        }

        public string HeadLabel { get; set; }

        public List<Node> Nodes { get; }
    }


}
