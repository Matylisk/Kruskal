using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lehre.Kruskal.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CSV Datei:");
            string csvDateiPfad = Console.ReadLine().Trim();
            try
            {
                Netzwerk netzwerk = Formatierer.LeseNetzwerk(csvDateiPfad);
                KruskalBaumGenerator baumGenerator = new KruskalBaumGenerator();
                Auswertung auswertung = baumGenerator.GeneriereBaumMitAuswertung(netzwerk);
                Console.WriteLine("Baum generiert!");
                Formatierer.SchreibeLoesung(csvDateiPfad, netzwerk);
                Console.WriteLine("Lösungsdatei abgelegt.");
                Console.WriteLine(auswertung.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Zum beenden Taste drücken.");
            Console.ReadKey();
        }
    }
}
