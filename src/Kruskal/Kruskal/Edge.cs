using System;

namespace Kruskal
{
    public class Edge : IComparable<Edge>
    {
        public Edge()
        {
            IsPartOfSolution = false;
        }

        public Edge(Node origin, Node destination, double weight) : this()
        {
            Origin = origin;
            Destination = destination;
            Weight = weight;
        }

        public Node Origin { get; set; }

        public Node Destination { get; set; }

        public double Weight { get; set; }

        public bool IsPartOfSolution { get; set; }

        public override string ToString()
        {
            return $"{Origin.Label}-{Destination.Label}: {Weight.ToString("N2")}";
        }

        public int CompareTo(Edge other)
        {
            return Weight.CompareTo(other.Weight);
        }
    }


}
