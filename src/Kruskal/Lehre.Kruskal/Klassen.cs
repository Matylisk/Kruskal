using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lehre.Kruskal
{
    public class Knoten
    {
        public string Name { get; set; }

        public Baum ZugewiesenerBaum { get; set; }

    }

    public class Baum
    {
        public Baum(Knoten ersterKnoten)
        {
            Knoten = new List<Knoten>();
            KnotenZufuegen(ersterKnoten);
            Name = ersterKnoten.Name;
        }

        public List<Knoten> Knoten { get; set; }

        public string Name { get; }

        public void KnotenZufuegen(Knoten neuerKnoten)
        {
            neuerKnoten.ZugewiesenerBaum = this;
            Knoten.Add(neuerKnoten);
        }

    }

    public class Kante
    {
        public Kante(Knoten quellKnoten, Knoten zielKnoten, double gewicht)
        {
            GehoertZurLoesung = false;
            Gewicht = gewicht;
            QuellKnoten = quellKnoten;
            ZielKnoten = zielKnoten;
        }

        public Knoten QuellKnoten { get; set; }

        public Knoten ZielKnoten { get; set; }

        public double Gewicht { get; set; }

        public bool GehoertZurLoesung { get; set; }

    }

    public class Netzwerk
    {
        public List<Knoten> Knoten { get; set; }
        public List<Kante> Kanten { get; set; }

        public Netzwerk()
        {
            Knoten = new List<Knoten>();
            Kanten = new List<Kante>();
        }

        public void NeueKante(Knoten quelle, Knoten ziel, double gewicht)
        {
            Kanten.Add(new Kante(quelle, ziel, gewicht));
        }

        public void NeueKante(string quelle, string ziel, double gewicht)
        {
            Knoten q = Knoten.Find(y => y.Name == quelle);
            Knoten z = Knoten.Find(y => y.Name == ziel);
            NeueKante(q, z, gewicht);
        }

        public List<Kante> MinimalSpannenderBaum
        {
            get
            {
                List<Kante> ergebnis = new List<Kante>();
                for (int i = 0; i < Kanten.Count; i++)
                {
                    if (Kanten[i].GehoertZurLoesung)
                    {
                        ergebnis.Add(Kanten[i]);
                    }
                }
                return ergebnis;
            }
        }

        public double MinimalesGesamtGewicht
        {
            get
            {
                double w = 0;
                List<Kante> baum = MinimalSpannenderBaum;
                foreach (Kante kante in baum)
                {
                    w = w + kante.Gewicht;
                }
                return w;
            }
        }

    }

    public class KruskalBaumGenerator
    {
        public List<Baum> Wald { get; set; }

        public void GeneriereBaum(Netzwerk netzwerk)
        {
            //1. Erzeuge Teilbäume
            ErzeugeWald(netzwerk);
            //2. Sortiere Kanten
            SortiereKanten(netzwerk);
            //3. Iteriere über die sortierten Kanten
            foreach (Kante kante in netzwerk.Kanten)
            {
                if (kante.QuellKnoten.ZugewiesenerBaum.Name !=
                    kante.ZielKnoten.ZugewiesenerBaum.Name)
                {
                    //Knoten gehören zu unterschiedlichen Teilbäumen! - Kante akzeptieren
                    kante.GehoertZurLoesung = true;
                    Vereinige(kante.QuellKnoten.ZugewiesenerBaum,
                        kante.ZielKnoten.ZugewiesenerBaum);
                }
                if (Wald.Count < 2)
                {
                    break;
                }
            }
        }

        public Auswertung GeneriereBaumMitAuswertung(Netzwerk netzwerk)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            GeneriereBaum(netzwerk);
            stopwatch.Stop();
            Auswertung auswertung = new Auswertung(stopwatch.Elapsed, netzwerk.Kanten.Count, netzwerk.Knoten.Count, netzwerk.MinimalesGesamtGewicht);
            return auswertung;
        }

        private void Vereinige(Baum baum1, Baum baum2)
        {
            foreach (Knoten knotenVonBaum2 in baum2.Knoten)
            {
                baum1.KnotenZufuegen(knotenVonBaum2);
            }
            baum2.Knoten.Clear();
            Wald.Remove(baum2);
        }

        private void ErzeugeWald(Netzwerk netzwerk)
        {
            Wald = new List<Baum>();
            foreach (var knoten in netzwerk.Knoten)
            {
                Wald.Add(new Baum(knoten));
            }
        }

        private void SortiereKanten(Netzwerk netzwerk)
        {
            netzwerk.Kanten.Sort((a, b) => a.Gewicht.CompareTo(b.Gewicht));
        }
    }

    public struct Auswertung
    {
        public Auswertung(TimeSpan rechenZeit, int kanten, int knoten, double gewicht)
        {
            Rechenzeit = rechenZeit;
            AnzahlKanten = kanten;
            AnzahlKnoten = knoten;
            Gesamtgewicht = gewicht;
        }

        public TimeSpan Rechenzeit { get; }

        public int AnzahlKanten { get; }

        public int AnzahlKnoten { get; }

        public double Gesamtgewicht { get; }

        public override string ToString()
        {
            return $"Knoten: {AnzahlKnoten}, Kanten: {AnzahlKanten}, Lösung: {Gesamtgewicht}\nRechenzeit: {Rechenzeit.TotalSeconds.ToString("N4")} Sekunden";
        }

    }


}
