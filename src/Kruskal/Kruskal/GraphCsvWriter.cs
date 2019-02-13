using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kruskal.IO
{
    public class GraphCsvWriter
    {
        public static void WriteGraph(string filePath, Graph graph)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("I;J;c(i,j);Tree(i,j)");
                foreach (var e in graph.Edges)
                {
                    writer.WriteLine($"{e.Origin.Label};{e.Destination.Label};{e.Weight.ToString("N4")};{e.IsPartOfSolution}");
                }
            }
        }
    }
}
