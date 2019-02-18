using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lehre.Kruskal.App
{
    public class Formatierer
    {
        //GeoWKT;Knoten1;Knoten2;Gewicht
        public static Netzwerk LeseNetzwerk(string csvDateiPfad)
        {
            Netzwerk n = new Netzwerk();
            Dictionary<string, Knoten> geleseneKnoten = new Dictionary<string, Knoten>();
            Dictionary<string, Kante> geleseneKanten = new Dictionary<string, Kante>();

            using (var reader = new StreamReader(csvDateiPfad, Encoding.Default))
            {
                //Kopfzeile
                reader.ReadLine();
                while (reader.Peek() >= 0)
                {
                    string zeile = reader.ReadLine().Trim();
                    if (!string.IsNullOrWhiteSpace(zeile))
                    {
                        string[] spaltenArray = zeile.Split(';');
                        string k1 = spaltenArray[1];
                        string k2 = spaltenArray[2];
                        if (double.TryParse(spaltenArray[3], out double w))
                        {
                            //check ob Kante (oder Gegenkante) bereits zugefügt
                            if (!(geleseneKanten.ContainsKey($"{k2}_{k1}") | geleseneKanten.ContainsKey($"{k1}_{k2}")))
                            {
                                //merke Knoten, falls noch nicht eingelesen
                                for (int i = 1; i <= 2; i++)
                                {
                                    if (!geleseneKnoten.TryGetValue(spaltenArray[i], out Knoten knoten))
                                    {
                                        knoten = new Knoten() { Name = spaltenArray[i] };
                                        geleseneKnoten.Add(knoten.Name, knoten);
                                    }
                                }
                                //merke Kante
                                Kante kante = new Kante(geleseneKnoten[k1], geleseneKnoten[k2], w);
                                geleseneKanten.Add($"{k1}_{k2}", kante);
                            }
                        }
                    }
                }
            }
            n.Knoten.AddRange(geleseneKnoten.Values);
            n.Kanten.AddRange(geleseneKanten.Values);
            return n;
        }

        //GeoWKT;Knoten1;Knoten2;Gewicht
        public static void SchreibeLoesung(string csvDateiPfad, Netzwerk n)
        {
            //loesung merken
            List<Kante> loesung = n.MinimalSpannenderBaum;
            Dictionary<string, Kante> KantenVerzeichnis = loesung.ToDictionary(loes => $"{loes.QuellKnoten.Name}_{loes.ZielKnoten.Name}");
            //Ausgabe string zusammenbauen
            StringBuilder sb = new StringBuilder();

            using (var reader = new StreamReader(csvDateiPfad, Encoding.Default))
            {
                //Kopfzeile der csv Datei
                sb.AppendLine($"{reader.ReadLine()};IstInLoesung");
                //lese kanten und ergänze Spalte mit Wert 1 für Lösungskanten 0 für Nichtlösungskanten
                while (reader.Peek() >= 0)
                {
                    string zeile = reader.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(zeile))
                        continue;

                    var spalten = zeile.Split(';');
                    if (KantenVerzeichnis.TryGetValue($"{spalten[1]}_{spalten[2]}", out Kante kante))
                    {
                        sb.AppendLine($"{zeile};1");
                    }
                    else
                    {
                        sb.AppendLine($"{zeile};0");
                    }
                }
            }

            //schreibe Ergebnis in Textdatei
            using (var writer = new StreamWriter(csvDateiPfad.Replace(".csv", "_ergebnis.csv"), false, Encoding.Default))
            {
                writer.Write(sb.ToString());
            }
        }
    }
}
