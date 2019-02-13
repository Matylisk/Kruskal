using System.Collections.Generic;
using System.Linq;

namespace Kruskal
{
    public class Graph
    {
        public Graph()
        {
            Edges = new List<Edge>();
            Nodes = new List<Node>();
            NeedsCalculation = false;
        }

        public void AddEdge(string originLabel, string destinationLabel, double weight)
        {
            var o = Nodes.Find(n => n.Label == originLabel);
            var d = Nodes.Find(n => n.Label == destinationLabel);
            AddEdge(o, d, weight);
        }

        public void AddEdge(Node origin, Node destination, double weight)
        {
            Edges.Add(new Edge() { Origin = origin, Destination = destination, Weight = weight });
        }

        public bool NeedsCalculation { get; set; }

        public List<Node> Nodes { get; set; }

        public List<Edge> Edges { get; set; }

        public List<Edge> KruskalEdges
        {
            get
            {
                return Edges.Where(w => w.IsPartOfSolution).ToList();
            }
        }

        public double MinimumSpanningTreeWeight
        {
            get
            {
                return KruskalEdges.Sum(e => e.Weight);
            }
        }

    }


}
