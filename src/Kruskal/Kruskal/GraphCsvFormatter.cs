using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Kruskal
{
    public class GraphCsvFormatter
    {
        //geo;node1;node2;distance
        public Graph ReadGraph(string csvFilePath)
        {
            Graph g = new Graph();
            Dictionary<string, Node> readNodes = new Dictionary<string, Node>();
            Dictionary<string, Edge> readEdges = new Dictionary<string, Edge>();

            using (var streamReader = new StreamReader(csvFilePath, Encoding.Default))
            {
                //header
                streamReader.ReadLine();
                while (streamReader.Peek() >= 0)
                {
                    string row = streamReader.ReadLine().Trim();
                    if (!string.IsNullOrWhiteSpace(row))
                    {
                        var rowArray = row.Split(';');
                        string n1 = rowArray[1];
                        string n2 = rowArray[2];
                        if (double.TryParse(rowArray[3], out double w))
                        {
                            //valid edge
                            if(!readEdges.ContainsKey($"{n2}_{n1}"))
                            {
                                if (!readNodes.TryGetValue(n1, out Node node1))
                                {
                                    node1 = new Node() { Label = n1 };
                                    readNodes.Add(n1, node1);
                                }
                                if (!readNodes.TryGetValue(n2, out Node node2))
                                {
                                    node2 = new Node() { Label = n2 };
                                    readNodes.Add(n2, node2);
                                }
                                var edge = new Edge(node1, node2, w);
                                readEdges.Add($"{n1}_{n2}", edge);
                            }
                        }
                    }
                }

            }

            foreach (var rn in readNodes)
            {
                g.Nodes.Add(rn.Value);
            }
            foreach (var re in readEdges)
            {
                g.Edges.Add(re.Value);
            }
            return g;
        }

        public void WriteSolution(string csvFilePath, Graph graph)
        {
            var kruskalSolution = graph.KruskalEdges;
            var solutionLookup = kruskalSolution
                .ToDictionary(sol => $"{sol.Origin.Label}_{sol.Destination.Label}");

            StringBuilder sb = new StringBuilder();

            using (var reader = new StreamReader(csvFilePath,Encoding.Default))
            {
                sb.AppendLine($"{reader.ReadLine()};MST");
                while(reader.Peek()>=0)
                {
                    string row = reader.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(row))
                        continue;

                    var rowArray = row.Split(';');
                    if (solutionLookup.TryGetValue($"{rowArray[1]}_{rowArray[2]}", out Edge edge))
                    {
                        sb.AppendLine($"{row};1");
                    }
                    else
                    {
                        sb.AppendLine($"{row};0");
                    }
                }
            }

            //write result
            using (var writer = new StreamWriter(csvFilePath.Replace(".csv", "_mst.csv"),false,Encoding.Default))
            {
                writer.Write(sb.ToString());
            }

        }

    }
}
