namespace Kruskal
{
    public class Edge
    {
        public Edge()
        {
            IsPartOfSolution = false;
        }

        public Node Origin { get; set; }

        public Node Destination { get; set; }

        public double Weight { get; set; }

        public bool IsPartOfSolution { get; set; }

    }


}
