using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kruskal
{
    public class KruskalTreeSolver
    {
        List<Tree> SetOfTrees = new List<Tree>();

        public ComputationStatistics Solve(Graph graph)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SetOfTrees = new List<Tree>();
            foreach (Node node in graph.Nodes)
            {
                MakeSet(node);
            }

            SortEdges(graph);

            foreach (Edge edge in graph.Edges)
            {                
                if(edge.Origin.AssignedTree.HeadLabel != edge.Destination.AssignedTree.HeadLabel)
                {
                    edge.IsPartOfSolution = true;
                    Union(edge.Origin, edge.Destination);
                }
                if (SetOfTrees.Count < 2)
                    break;
            }
            stopwatch.Stop();
            return new ComputationStatistics(stopwatch.Elapsed,graph.Nodes.Count,graph.Edges.Count);
        }

        private void SortEdges(Graph graph)
        {
            //sort the edges by their weight
            graph.Edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));
            //implement icomparer for edge:
            //graph.Edges.Sort();
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
