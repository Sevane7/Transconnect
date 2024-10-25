using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class GrapheDijkstra
    {
        public Dictionary<string, Dictionary<string, double>> graphe;
        public GrapheDijkstra(List<Tuple<string, string, double>> data)
        {
            // Création du graphe
            graphe = new Dictionary<string, Dictionary<string, double>>();

            foreach (var d in data)
            {
                // Vérification de l'unicité des clés
                if (!graphe.ContainsKey(d.Item1)) graphe[d.Item1] = new Dictionary<string, double>();
                if (!graphe.ContainsKey(d.Item2)) graphe[d.Item2] = new Dictionary<string, double>();

                // remplissage des poids (double <=> Item3) dans le dictionnaire
                graphe[d.Item1][d.Item2] = d.Item3;
                graphe[d.Item2][d.Item1] = d.Item3;
            }
        }
        public Dictionary<string, Dictionary<string, double>> Graphe { get { return this.graphe; } set { this.graphe = value; } }
        public List<string> CourtChemin(string depart, string arrivee)
        {
            Dictionary<string, double> distances = new Dictionary<string, double>();
            // Initialisation de toutes les distances à l'infini sauf au départ = 0
            foreach (var ville in graphe.Keys) distances[ville] = (ville == depart) ? 0 : double.PositiveInfinity;

            // Initialisation des predecesseurs (dict) et des villes non visitées (HashSet)
            Dictionary<string, string> predecessseur = new Dictionary<string, string>();
            HashSet<string> nonvisite = new HashSet<string>(graphe.Keys);

            // Dijkstra
            while (nonvisite.Count() > 0)
            {
                // Selectionne la ville la plus proche non vistée
                string current = nonvisite.OrderBy(ville => distances[ville]).First();
                nonvisite.Remove(current);

                // Verification du point d'arrivee
                if (current == arrivee) break;
                // Sinon mise à jour des voisins
                else
                {
                    foreach (var voisins in graphe[current]) //  graphe[current] de type Dictionnary<string, double>
                    {
                        double distance = distances[current] + voisins.Value;
                        if (distance < distances[voisins.Key])
                        {
                            distances[voisins.Key] = distance;
                            predecessseur[voisins.Key] = current;
                        }
                    }
                }
            }

            // Chemin à retourner
            List<string> chemin = new List<string>();
            string current_ville = arrivee;

            // On part de l'arrivée en utilisant predecessors
            while (current_ville != depart)
            {
                chemin.Insert(0, current_ville);
                current_ville = predecessseur[current_ville];
            }
            chemin.Insert(0, depart);
            return chemin;
        }
        public double DistanceChemin(List<string> chemin)
        {
            double distance = 0;
            for (int i = 0; i < chemin.Count() - 1; i++)
            {
                //Console.WriteLine($"{chemin[i]} - > {chemin[i + 1]} : {graphe[chemin[i]][chemin[i + 1]]} km");
                distance += graphe[chemin[i]][chemin[i + 1]];
            }
            return distance;
        }
    }
}
