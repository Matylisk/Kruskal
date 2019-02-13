using Kruskal.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Kruskal.Tests
{
    [TestClass]
    public class Kruskal_Algorithm
    {
        [TestMethod]
        public void CalculateExampleMst()
        {
            //get a sample Graph
            Graph graph = GetSampleGraph();
            KruskalTreeSolver solver = new KruskalTreeSolver();
            solver.Solve(graph);
            GraphCsvWriter.WriteGraph(@"C:\temp\graph.csv", graph);
            Assert.AreEqual(37d, graph.MinimumSpanningTreeWeight);
        }

        //Example from Cormen
        public Graph GetSampleGraph()
        {
            Graph g = new Graph();
            string[] nodeLabels = new string[]
            {
                "A","B","C","D","E","F","G","H","I"
            };

            for (int i = 0; i < nodeLabels.Length; i++)
            {
                g.Nodes.Add(new Node() { Label = nodeLabels[i] });
            }

            g.AddEdge("A", "B", 4);
            g.AddEdge("A", "H", 8);
            g.AddEdge("B", "H", 11);
            g.AddEdge("B", "C", 8);
            g.AddEdge("C", "I", 2);
            g.AddEdge("C", "F", 4);
            g.AddEdge("C", "D", 7);
            g.AddEdge("I", "H", 7);
            g.AddEdge("H", "G", 1);
            g.AddEdge("I", "G", 6);
            g.AddEdge("G", "F", 2);
            g.AddEdge("D", "F", 14);
            g.AddEdge("F", "E", 10);
            g.AddEdge("D", "E", 9);

            g.NeedsCalculation = true;
            return g;
        }
               
    }
}
