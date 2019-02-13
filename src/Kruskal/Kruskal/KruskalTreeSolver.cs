using System.Collections.Generic;
using System.Linq;

namespace Kruskal
{
    public class KruskalTreeSolver
    {
        List<Tree> SetOfTrees = new List<Tree>();

        public void Solve(Graph graph)
        {
            SetOfTrees = new List<Tree>();
            foreach (Node node in graph.Nodes)
            {
                MakeSet(node);
            }

            var sortedEdges = graph.Edges.OrderBy(e => e.Weight).ToArray();
            foreach (Edge edge in sortedEdges)
            {
                
                if(edge.Origin.AssignedTree.HeadLabel != edge.Destination.AssignedTree.HeadLabel)
                {
                    edge.IsPartOfSolution = true;
                    Union(edge.Origin, edge.Destination);
                }
                if (SetOfTrees.Count < 2)
                    break;
            }
        }

        private void Union(Node origin, Node destination)
        {
            Tree t1 = origin.AssignedTree;
            Tree t2 = destination.AssignedTree;

            foreach (Node n in t2.Nodes)
            {
                t1.AddNode(n);
            }
            t2.Nodes.Clear();
            SetOfTrees.Remove(t2);
        }

        private void MakeSet(Node node)
        {
            Tree tree = new Tree(node);
            SetOfTrees.Add(tree);
        }

    }

}
