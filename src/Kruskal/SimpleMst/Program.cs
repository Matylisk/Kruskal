using Kruskal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMst
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CSV Datei:");
            string file = Console.ReadLine().Trim();
            GraphCsvFormatter f = new GraphCsvFormatter();
            var g = f.ReadGraph(file);
            KruskalTreeSolver kruskalTreeSolver = new KruskalTreeSolver();
            ComputationStatistics stats = kruskalTreeSolver.Solve(g);
            f.WriteSolution(file, g);
            Console.WriteLine($"Finished.\n{stats.ToString()}");
            Console.WriteLine("Hit key to exit..");
            Console.ReadKey();
        }
    }
}
