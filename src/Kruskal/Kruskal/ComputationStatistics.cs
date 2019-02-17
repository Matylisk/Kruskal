using System;

namespace Kruskal
{

    public struct ComputationStatistics
    {
        public ComputationStatistics(TimeSpan computationTime, int numberOfNodes, int numberOfEdges)
        {
            ComputationTime = computationTime;
            NumberOfEdges = numberOfEdges;
            NumberOfNodes = numberOfNodes;
        }

        public int NumberOfNodes { get; }

        public int NumberOfEdges { get; }

        public TimeSpan ComputationTime { get; }

        public override string ToString()
        {
            return $"Edges: {NumberOfEdges.ToString("N0")}, Nodes: {NumberOfNodes.ToString("N0")}, Processingtime: {ComputationTime.TotalSeconds.ToString("N4")} seconds.";
        }

    }


}
